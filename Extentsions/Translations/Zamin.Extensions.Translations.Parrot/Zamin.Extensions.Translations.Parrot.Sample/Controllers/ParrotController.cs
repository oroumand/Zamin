using Microsoft.AspNetCore.Mvc;
using Zamin.Extensions.Translations.Abstractions;

namespace Zamin.Extensions.Translations.Parrot.Sample.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ParrotController : ControllerBase
{
    private readonly ITranslator _translator;

    public ParrotController(ITranslator translator)
    {
        _translator = translator;
    }

    [HttpGet(Name = "GetTranslation")]
    public IActionResult Get(string key)
    {
        return Ok(_translator.GetString(key));
    }
}