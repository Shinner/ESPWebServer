using AniyaWebService.MQTT;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace AniyaWebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AniyaController : ControllerBase
    {
        private readonly IAniyaMqttServer _aniyaMqttServer;

        private readonly ILogger<WeatherForecastController> _logger;

        public AniyaController(ILogger<WeatherForecastController> logger, IAniyaMqttServer aniyaMqttServer)
        {
            _logger = logger;
            _aniyaMqttServer=aniyaMqttServer;
        }

        [HttpPost]
        public async Task TestMqtt(string client,string topic,string payload)
        {
            await _aniyaMqttServer.PublishMessageFromBroker(client, topic, payload);
        }
    }
}