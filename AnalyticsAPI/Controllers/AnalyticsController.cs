using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnalyticsAPI.Entities;
using OpenTracing;

namespace AnalyticsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<AnalyticsController> _logger;
        private ITracer _tracer;

        public AnalyticsController(ILogger<AnalyticsController> logger, ITracer tracer)
        {
            _logger = logger;
            _tracer = tracer;
        }

        [HttpGet]
        [Route("HealthCheck")]
        public string HealthCheck()
        {
            return "Analytics Service is Up!!";
        }


        [HttpPost]
        [Route("AnalyzePlan")]
        public int AnalyzePlan(Plan plan)
        {
            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
            var operationName = "AnalyzePlan";
            var builder = _tracer.BuildSpan(operationName);

            using (var scope = builder.StartActive(true))
            {
                var span = scope.Span;

                var log = $"Analytics started - ";
                span.Log(log);
            }

            _logger.LogInformation("Running Analytics to return POS");

            int result = BusinessLayer.AnalyzePlan.CalculatePos(plan);
            return result;
        }
    }
}
