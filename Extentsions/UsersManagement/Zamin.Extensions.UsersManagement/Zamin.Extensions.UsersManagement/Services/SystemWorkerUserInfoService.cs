using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zamin.Extensions.ObjectMappers.AutoMapper.Options;
using Zamin.Extentions.UsersManagement.Abstractions;

namespace Zamin.Extensions.UsersManagement.Services;

public class SystemWorkerUserInfoService : IUserInfoService
{
    private readonly ILogger<SystemWorkerUserInfoService> _logger;
    private readonly SystemWorkerUserInfoOption _configuration;

    public SystemWorkerUserInfoService(ILogger<SystemWorkerUserInfoService> logger, IOptions<SystemWorkerUserInfoOption> configuration)
    {
        _logger = logger;
        _configuration = configuration.Value;

        _logger.LogInformation("SystemWorkerUserInfo Translator Start working with Configuration {configuration}", _configuration);
    }

    public string GetUsername() => _configuration.UserName;

    public string UserId() => _configuration.UserId;

    public string GetUserAgent() => _configuration.UserAgent;

    public string GetUserIp() => _configuration.UserIp;

    public string GetFirstName() => _configuration.FirstName;

    public string GetLastName() => _configuration.LastName;

    public bool HasAccess(string access) => true;

    public bool IsCurrentUser(string userId) => true;

    public string? GetClaim(string claimType) => null;
}