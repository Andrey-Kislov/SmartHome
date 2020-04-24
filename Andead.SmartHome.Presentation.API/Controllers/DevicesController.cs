using System.Text;
using Andead.SmartHome.Constants;
using Andead.SmartHome.Mqtt;
using Microsoft.AspNetCore.Mvc;

namespace Andead.SmartHome.Presentation.API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DevicesController : SmartHomeBaseController
    {
        private readonly MqttService _mqttService;

        public DevicesController(MqttService mqttService)
        {
            _mqttService = mqttService;
        }

        [HttpPost("[action]")]
        public IActionResult Set(string deviceName, string message)
        {
            _mqttService.Publish($"{Settings.MQTT_BASE_TOPIC}/{deviceName}/set", Encoding.ASCII.GetBytes(message));

            return Ok(null);
        }
    }
}
