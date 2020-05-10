using System;
using System.Text;
using Andead.SmartHome.Constants;
using Andead.SmartHome.Presentation.API.Models;
using Andead.SmartHome.Services;
using Andead.SmartHome.UnitOfWork.Entities;
using Andead.SmartHome.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Andead.SmartHome.Presentation.API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DevicesController : SmartHomeBaseController
    {
        private readonly MqttService _mqttService;
        private readonly IRepositoryFactory _repositoryFactory;

        public DevicesController(MqttService mqttService, IRepositoryFactory repositoryFactory)
        {
            _mqttService = mqttService;
            _repositoryFactory = repositoryFactory;
        }

        [HttpPost("[action]")]
        public IActionResult Add(AddDeviceCommand command)
        {
            try
            {
                using var repository = _repositoryFactory.Create();
                repository.Add(new Device
                {
                    DeviceName = command.DeviceName,
                    IeeeAddress = command.IeeeAddress,
                    FriendlyName = command.FriendlyName,
                    Type = command.Type,
                    NetworkAddress = command.NetworkAddress,
                    ManufacturerId = command.ManufacturerId,
                    ManufacturerName = command.ManufacturerName,
                    PowerSource = command.PowerSource,
                    ModelId = command.ModelId,
                    Status = command.Status,
                });
                repository.Commit();

                return Ok(null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]")]
        public IActionResult Get()
        {
            try
            {
                using var repository = _repositoryFactory.Create();
                var result = repository.Get<Device>().ToArray();

                return Ok(null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("[action]")]
        public IActionResult Set(string deviceName, string message)
        {
            _mqttService.Publish($"{Settings.MQTT_BASE_TOPIC}/{deviceName}/set", Encoding.ASCII.GetBytes(message));

            return Ok(null);
        }
    }
}
