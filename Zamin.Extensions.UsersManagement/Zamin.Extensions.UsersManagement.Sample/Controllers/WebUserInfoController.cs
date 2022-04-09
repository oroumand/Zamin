using Microsoft.AspNetCore.Mvc;
using Zamin.Extentions.UsersManagement.Abstractions;

namespace Zamin.Extensions.UsersManagement.Sample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WebUserInfoController : ControllerBase
{
    private readonly IUserInfoService _userInfoService;

    public WebUserInfoController(IUserInfoService userInfoService)
    {
        _userInfoService = userInfoService;
    }

    [HttpGet("Username")]
    public IActionResult GetUsername()
    {
        return Ok(_userInfoService.GetUsername());
    }

    [HttpGet("UserId")]
    public IActionResult GetUserId()
    {
        return Ok(_userInfoService.UserId());
    }
}
