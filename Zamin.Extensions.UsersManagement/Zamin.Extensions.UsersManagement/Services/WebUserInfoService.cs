using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Zamin.Extensions.UsersManagement.Extensions;
using Zamin.Extentions.UsersManagement.Abstractions;

namespace Zamin.Extensions.UsersManagement.Services;

public class WebUserInfoService : IUserInfoService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string AccessList = "";

    public WebUserInfoService(IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor == null || httpContextAccessor.HttpContext == null)
            throw new ArgumentNullException(nameof(httpContextAccessor));

        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserAgent()
        => _httpContextAccessor.HttpContext.Request.Headers["User-Agent"];

    public string GetUserIp()
        => _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

    public int UserId()
        => int.Parse(_httpContextAccessor.HttpContext.User?.GetClaim(ClaimTypes.NameIdentifier) ?? "0");

    public string GetUsername()
        => _httpContextAccessor.HttpContext.User?.GetClaim(ClaimTypes.Name);

    public string GetFirstName()
        => _httpContextAccessor.HttpContext.User?.GetClaim(ClaimTypes.GivenName);

    public string GetLastName()
        => _httpContextAccessor.HttpContext.User?.GetClaim(ClaimTypes.Surname);

    public bool IsCurrentUser(string userId)
    {
        return string.Equals(UserId().ToString(), userId, StringComparison.OrdinalIgnoreCase);
    }

    [Obsolete]
    public virtual bool HasAccess(string accessKey)
    {
        var result = false;

        if (!string.IsNullOrWhiteSpace(accessKey))
        {
            var accessList = _httpContextAccessor.HttpContext.User?.GetClaim(AccessList)?.Split(',').ToList() ?? new List<string>();
            result = accessList.Any(key => string.Equals(key, accessKey, StringComparison.OrdinalIgnoreCase));
        }

        return result;
    }
}
