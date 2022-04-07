using Microsoft.AspNetCore.Mvc;
using Zamin.Extensions.DependentyInjection.Sample.Services;

namespace Zamin.Extensions.DependentyInjection.Sample.Controller;

[Route("api/[controller]")]
[ApiController]
public class GuidGeneratorController : ControllerBase
{
    private readonly IGetGuidSingletoneService _getRandomNumberSingletoneService;

    public GuidGeneratorController(IGetGuidSingletoneService getRandomNumberSingletoneService)
    {
        _getRandomNumberSingletoneService = getRandomNumberSingletoneService;
    }

    [HttpGet("GetRandomNumberTransient")]
    public async Task<IActionResult> GetRandomNumberTransient([FromServices] IGetGuidTransientService service1,
                                                              [FromServices] IGetGuidTransientService service2)
        => Ok(string.Format("1 : {0} , 2 : {1}", service1.Execute(), service2.Execute()));

    [HttpGet("GetRandomNumberScope")]
    public async Task<IActionResult> GetRandomNumberScope([FromServices] IGetGuidScopeService service1,
                                                          [FromServices] IGetGuidScopeService service2)
        => Ok(string.Format("1 : {0} , 2 : {1}",
            service1.Execute(),
            service2.Execute()));

    [HttpGet("GetRandomNumberSingletone")]
    public async Task<IActionResult> GetRandomNumberSingletone([FromServices] IGetGuidSingletoneService service1,
                                                               [FromServices] IGetGuidSingletoneService service2)
        => Ok(string.Format("1 : {0} , 2 : {1}", service1.Execute(), service2.Execute()));
}
