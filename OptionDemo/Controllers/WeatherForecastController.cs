using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OptionDemo.Extensions;

namespace OptionDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;


        private readonly IOptions<RabbitMQConfiguration> _options;


        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IOptions<RabbitMQConfiguration> options)
        {
            _logger = logger;
            _options = options;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet]
        public ActionResult<string> Get()
        {
            string res = _options.Value.RabbitHost + "\n" +
                _options.Value.RabbitPort + "\n" +
                _options.Value.RabbitUserName + "\n" +
                _options.Value.RabbitPassword + "\n" +
                _options.Value.ExchangeName + "\n" +
                _options.Value.QueueName + "\n" +
                _options.Value.RouteKey + "\n";

            return res;
        }
    }
}
