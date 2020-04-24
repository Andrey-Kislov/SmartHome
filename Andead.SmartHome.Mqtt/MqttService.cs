using System;
using System.Text;
using System.Threading;
using MQTTnet;
using MQTTnet.Server;
using Andead.SmartHome.Mqtt.Interfaces;
using System.Collections.Generic;
using System.Collections.Concurrent;
using MQTTnet.Client.Receiving;
using Microsoft.Extensions.Configuration;
using Andead.SmartHome.Constants;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Andead.SmartHome.Mqtt
{
    public class MqttService : IService, IDisposable
    {
        private readonly BlockingCollection<MqttApplicationMessageReceivedEventArgs> _incomingMessages = new BlockingCollection<MqttApplicationMessageReceivedEventArgs>();
        private readonly Dictionary<string, MqttSubscriber> _subscribers = new Dictionary<string, MqttSubscriber>();
        private readonly SystemCancellationToken _systemCancellationToken;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        private IMqttServer _mqttServer;

        public MqttService(SystemCancellationToken systemCancellationToken, IConfiguration configuration, ILogger<MqttService> logger)
        {
            _systemCancellationToken = systemCancellationToken;
            _configuration = configuration;
            _logger = logger;
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

        private void ProcessIncomingMqttMessages()
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

                    var payload = Encoding.ASCII.GetString(message.ApplicationMessage.Payload);

                    _logger.LogInformation($"{message.ApplicationMessage.Topic}: {payload}");

                    try
                    {
                        var json = JObject.Parse(payload);
                        var click = json.GetValue("click");

                        if (click != null)
                        {
                            _logger.LogInformation($"Clicked: {click}");
                        }
                    }
                    catch (JsonReaderException)
                    { }
                    
                    var affectedSubscribers = new List<MqttSubscriber>();
                    lock (_subscribers)
                    {
                        foreach (var subscriber in _subscribers.Values)
                        {
                            if (subscriber.IsFilterMatch(message.ApplicationMessage.Topic))
                            {
                                affectedSubscribers.Add(subscriber);
                            }
                        }
                    }

                    foreach (var subscriber in affectedSubscribers)
                    {
                        TryNotifySubscriber(subscriber, message);
                    }
                }
                catch (ThreadAbortException)
                {
                }
                catch (OperationCanceledException)
                {
                }
                catch (Exception exception)
                {
                    
                }
            }
        }

        private void OnApplicationMessageReceived(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            _incomingMessages.Add(eventArgs);
        }

        private void TryNotifySubscriber(MqttSubscriber subscriber, MqttApplicationMessageReceivedEventArgs message)
        {
            try
            {
                subscriber.Notify(message);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception exception)
            {
            }
        }

        public void Publish(string topic, byte[] payload)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .Build();

            _mqttServer.PublishAsync(message).GetAwaiter().GetResult();
        }

        public string Subscribe(string uid, string topicFilter, Action<MqttApplicationMessageReceivedEventArgs> callback)
        {
            if (string.IsNullOrEmpty(uid))
            {
                uid = Guid.NewGuid().ToString("D");
            }

            lock (_subscribers)
            {
                _subscribers[uid] = new MqttSubscriber(uid, topicFilter, callback);
            }

            return uid;
        }

        public void Unsubscribe(string uid)
        {
            lock (_subscribers)
            {
                _subscribers.Remove(uid);
            }
        }
    }
}
