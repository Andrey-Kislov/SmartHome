using System;
using System.Text;
using System.Threading;
using MQTTnet;
using MQTTnet.Server;
using System.Collections.Generic;
using System.Collections.Concurrent;
using MQTTnet.Client.Receiving;
using Microsoft.Extensions.Configuration;
using Andead.SmartHome.Constants;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Andead.SmartHome.Services.Interfaces;
using Andead.SmartHome.Mqtt;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using Andead.SmartHome.UnitOfWork.Interfaces;
using Andead.SmartHome.UnitOfWork.Entities;
using Andead.SmartHome.UnitOfWork.Extensions;
using Microsoft.AspNetCore.SignalR;
using Andead.SmartHome.Presentation.Hubs;

namespace Andead.SmartHome.Services
{
    public class MqttService : IService, IDisposable
    {
        private readonly BlockingCollection<MqttApplicationMessageReceivedEventArgs> _incomingMessages = new BlockingCollection<MqttApplicationMessageReceivedEventArgs>();
        private readonly Dictionary<string, MqttSubscriber> _subscribers = new Dictionary<string, MqttSubscriber>();
        private readonly SystemCancellationToken _systemCancellationToken;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IHubContext<DeviceStateHub, IDeviceStateHub> _hubContext;

        private IMqttServer _mqttServer;

        public MqttService(
            SystemCancellationToken systemCancellationToken,
            IConfiguration configuration,
            ILogger<MqttService> logger,
            IRepositoryFactory repositoryFactory,
            IHubContext<DeviceStateHub, IDeviceStateHub> hubContext)
        {
            _systemCancellationToken = systemCancellationToken;
            _configuration = configuration;
            _logger = logger;
            _repositoryFactory = repositoryFactory;
            _hubContext = hubContext;
        }

        public void Dispose()
        {
            _incomingMessages.Dispose();
        }

        public void Start()
        {
            var mqttFactory = new MqttFactory();
            _mqttServer = mqttFactory.CreateMqttServer();

            _mqttServer.UseApplicationMessageReceivedHandler(new MqttApplicationMessageReceivedHandlerDelegate(e => OnApplicationMessageReceived(e)));

            var serverOptions = new MqttServerOptionsBuilder()
                .WithDefaultEndpointPort(int.TryParse(_configuration[Settings.MQTT_SERVER_LISTEN_PORT], out int mqttListenPort) ? mqttListenPort : 1883)
                .WithPersistentSessions();

            _mqttServer.StartAsync(serverOptions.Build()).GetAwaiter().GetResult();

            Thread workerThread = new Thread(ProcessIncomingMqttMessages)
            {
                Name = nameof(MqttService),
                IsBackground = true
            };

            workerThread.Start();
        }

        private string GetIeeeAddress(string payload)
        {
            JObject device = null;

            try
            {
                var json = JObject.Parse(payload);
                device = JObject.Parse(json?.GetValue("device")?.ToString() ?? String.Empty);
            }
            catch (JsonReaderException)
            { }

            return device?.GetValue("ieeeAddr")?.ToString() ?? String.Empty;
        }

        private static readonly string[] TopicsToIgnore =
        {
            "/bridge/state",
            "/bridge/config",

            "/bridge/config/last_seen",
			"/bridge/config/log_level",
			"/bridge/config/permit_join",
			"/bridge/config/remove",
			"/bridge/config/force_remove",
			"/bridge/config/rename",
			"/bridge/configure",
			"/bridge/ota_update"
		};

        private async void ProcessIncomingMqttMessages()
        {
            var cancellationToken = _systemCancellationToken.Token;

            while (!cancellationToken.IsCancellationRequested)
            {
                MqttApplicationMessageReceivedEventArgs message = null;
                try
                {
                    message = _incomingMessages.Take(cancellationToken);

                    if (message == null || cancellationToken.IsCancellationRequested)
                        return;

                    if (TopicsToIgnore.Any(x => message.ApplicationMessage.Topic.EndsWith(x)))
                        continue;

                    var payload = Encoding.ASCII.GetString(message.ApplicationMessage.Payload);

                    var ieeeAddress = GetIeeeAddress(payload);
                    _logger.LogInformation($"{ieeeAddress}: {payload}");

                    using var repository = _repositoryFactory.Create();
                    var device = repository.Get<Device>().ByIeeeAddress(ieeeAddress).FirstOrDefault();

                    if (device != null)
                    {
                        var json = JObject.Parse(payload);

                        foreach (var attribute in device.Model.Attributes)
                        {
                            var attributeValue = json.GetValue(attribute.AttributeName)?.ToString();
                            if (!string.IsNullOrEmpty(attributeValue))
                            {
                                await _hubContext.Clients.All.NewEvent(DateTimeOffset.Now, device.Id, attribute.Id, attributeValue);

                                repository.Add(new ZigbeeEvent
                                {
                                    DeviceId = device.Id,
                                    DeviceAttributeId = attribute.Id,
                                    DeviceAttributeValue = attributeValue
                                });
                                repository.Commit();
                            }
                        }
                    }

                    //try
                    //{
                    //    var json = JObject.Parse(payload);
                    //    var click = json.GetValue("click");

                    //    if (click != null)
                    //    {
                    //        _logger.LogInformation($"Clicked: {click}");
                    //    }
                    //}
                    //catch (JsonReaderException)
                    //{ }

                    //var affectedSubscribers = new List<MqttSubscriber>();
                    //lock (_subscribers)
                    //{
                    //    foreach (var subscriber in _subscribers.Values)
                    //    {
                    //        if (subscriber.IsFilterMatch(message.ApplicationMessage.Topic))
                    //        {
                    //            affectedSubscribers.Add(subscriber);
                    //        }
                    //    }
                    //}

                    //foreach (var subscriber in affectedSubscribers)
                    //{
                    //    TryNotifySubscriber(subscriber, message);
                    //}
                }
                catch (ThreadAbortException)
                {
                }
                catch (OperationCanceledException)
                {
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }

        private void OnApplicationMessageReceived(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            _incomingMessages.Add(eventArgs);
        }

        

        public void Publish(string topic, byte[] payload)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .Build();

            _mqttServer.PublishAsync(message).GetAwaiter().GetResult();
        }

        //private void TryNotifySubscriber(MqttSubscriber subscriber, MqttApplicationMessageReceivedEventArgs message)
        //{
        //    try
        //    {
        //        subscriber.Notify(message);
        //    }
        //    catch (OperationCanceledException)
        //    {
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //public string Subscribe(string uid, string topicFilter, Action<MqttApplicationMessageReceivedEventArgs> callback)
        //{
        //    if (string.IsNullOrEmpty(uid))
        //    {
        //        uid = Guid.NewGuid().ToString("D");
        //    }

        //    lock (_subscribers)
        //    {
        //        _subscribers[uid] = new MqttSubscriber(uid, topicFilter, callback);
        //    }

        //    return uid;
        //}

        //public void Unsubscribe(string uid)
        //{
        //    lock (_subscribers)
        //    {
        //        _subscribers.Remove(uid);
        //    }
        //}
    }
}
