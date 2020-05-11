using System;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Andead.SmartHome.Constants;
using Andead.SmartHome.Presentation.API.Models;
using Andead.SmartHome.Services;
using Andead.SmartHome.UnitOfWork.Entities;
using Andead.SmartHome.UnitOfWork.Interfaces;
using Andead.SmartHome.UnitOfWork.Extensions;

namespace Andead.SmartHome.Presentation.API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DevicesController : SmartHomeBaseController
    {
        private readonly MqttService _mqttService;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;

        public DevicesController(MqttService mqttService, IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _mqttService = mqttService;
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
    }

        [HttpPost("models/add")]
        public IActionResult AddModel(AddModelCommand command)
        {
            try
            {
                using var repository = _repositoryFactory.Create();
                repository.Add(new DeviceModel
                {
                    ModelName = command.ModelName,
                    ModelId = command.ModelId,
                    ImageUrl = command.ImageUrl
                });
                repository.Commit();

                return Ok(null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("attributes/add")]
        public IActionResult AddAttribute(AddAttributeCommand command)
        {
            try
            {
                using var repository = _repositoryFactory.Create();
                repository.Add(new DeviceAttribute
                {
                    AttributeFriendlyName = command.AttributeFriendlyName,
                    AttributeName = command.AttributeName,
                    DeviceModelId = command.DeviceModelId,
                    AttributeType = command.AttributeType
                });
                repository.Commit();

                return Ok(null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("devices/add")]
        public IActionResult AddDevice(AddDeviceCommand command)
        {
            try
            {
                using var repository = _repositoryFactory.Create();
                repository.Add(new Device
                {
                    DeviceName = command.DeviceName,
                    UserId = command.UserId,
                    IeeeAddress = command.IeeeAddress,
                    FriendlyName = command.FriendlyName,
                    Type = command.Type,
                    NetworkAddress = command.NetworkAddress,
                    ManufacturerId = command.ManufacturerId,
                    ManufacturerName = command.ManufacturerName,
                    PowerSource = command.PowerSource,
                    ModelId = command.ModelId,
                    Status = command.Status
                });
                repository.Commit();

                return Ok(null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("devices/get")]
        public IActionResult GetDevices()
        {
            try
            {
                using var repository = _repositoryFactory.Create();
                var result = _mapper.Map<DeviceDto[]>(repository.Get<Device>().ToArray());

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("devices/get")]
        public IActionResult GetDevices(int userId)
        {
            try
            {
                using var repository = _repositoryFactory.Create();
                var result = _mapper.Map<DeviceDto[]>(repository.Get<Device>().ByUserId(userId).ToArray());

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("devices/set")]
        public IActionResult Set(string deviceName, string message)
        {
            _mqttService.Publish($"{Settings.MQTT_BASE_TOPIC}/{deviceName}/set", Encoding.ASCII.GetBytes(message));

            return Ok(null);
        }
    }
}
