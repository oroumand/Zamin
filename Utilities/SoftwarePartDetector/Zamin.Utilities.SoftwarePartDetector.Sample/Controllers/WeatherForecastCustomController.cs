using Microsoft.AspNetCore.Mvc;
using Zamin.Utilities.SoftwarePartDetector.Attributes;

namespace Zamin.Utilities.SoftwarePartDetector.Sample.Controllers;

[ApiController]
[Route("[controller]")]
[SoftwarePartControllerOption(service: "SoftwareServiceCustomService", module: "SoftwareServiceCustomModule")]
public class WeatherForecastCustomController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet("GetWeatherForecastCustom")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();
    }
}