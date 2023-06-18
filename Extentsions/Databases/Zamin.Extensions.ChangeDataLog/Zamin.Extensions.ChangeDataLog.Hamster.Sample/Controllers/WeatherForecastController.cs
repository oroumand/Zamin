using Microsoft.AspNetCore.Mvc;
using Zamin.Extensions.ChangeDataLog.Hamster.Sample.DAL;

namespace Zamin.Extensions.ChangeDataLog.Hamster.Sample.Controllers
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
        private readonly HamsterTestContext _hamsterTestContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, HamsterTestContext hamsterTestContext)
        {
            _logger = logger;
            _hamsterTestContext = hamsterTestContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var items = _hamsterTestContext.People.ToList();
            foreach (var item in items)
            {
                item.Name = item.Name + " " + DateTime.Now.Ticks;
            }
            _hamsterTestContext.Add(new Person
            {
                Name = "AmirHossein",
                FullName = "Tayyare"
            });
            _hamsterTestContext.SaveChanges();



            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}