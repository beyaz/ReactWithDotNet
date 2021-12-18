using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactDotNet.Demo
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "aaaFreezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ReactElement Get()
        {
            return new div("C4") { text = "Aloha", style =
            {
                Width = "5px", 
                Display = Display.Flex, 
                Height = "6px",
                AlignContent = AlignContent.FlexStart
            } };


            //    new div("croot")
            //{
            //    new div("C1"),
            //    new div("C2"),
            //    new div("C3"),
            //    new div("C4") { text = "Aloha", style = { Width = "5px", Display = Display.Flex}}
            //};
        }
    }
}
