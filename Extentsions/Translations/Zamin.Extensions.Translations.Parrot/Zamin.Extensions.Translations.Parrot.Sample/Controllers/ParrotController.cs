using Microsoft.AspNetCore.Mvc;
using Zamin.Extentions.Translations.Abstractions;

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
        var temp = _translator["Required", "Description"];
       var aa =  _translator["Required", "Description", "500"];
        return Ok(_translator.GetString(key));
    }
}