using Microsoft.AspNetCore.Mvc;
using Zamin.Extensions.ObjectMappers.Abstractions;
using Zamin.Extensions.ObjectMappers.Mapster.Sample.Dtos;
using Zamin.Extensions.ObjectMappers.Mapster.Sample.Model;
using Zamin.Extensions.ObjectMappers.Mapster.Services;

namespace Zamin.Extensions.ObjectMappers.Mapster.Sample.Controllers;

[ApiController]
[Route("api/person")]
public class WeatherForecastController : ControllerBase
{
    private static readonly Person _person = new()
    {
        Id = 1,
        Name = "Alireza",
        LastName = "Orumand"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IObjectMapper _objectMapper;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, Services.IObjectMapper objectMapper)
    {
        _logger = logger;
        _objectMapper = objectMapper;
    }

    [HttpGet("Person")]
    public PersonDto Get()
    {
        return _objectMapper.Map<Person, PersonDto>(_person);
    }

    [HttpGet("FullName")]
    public PersonFullNameDto GetFullName()
    {
        var dto = _objectMapper.Map<PersonFullNameDto>(_person);
        return dto;
    }
}