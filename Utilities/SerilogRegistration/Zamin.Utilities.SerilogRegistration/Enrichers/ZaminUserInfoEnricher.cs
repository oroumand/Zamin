using Serilog.Core;
using Serilog.Events;
using Zamin.Extensions.UsersManagement.Abstractions;

namespace Zamin.Utilities.SerilogRegistration.Enrichers;

public class ZaminUserInfoEnricher : ILogEventEnricher
{
    private readonly IUserInfoService _userInfoService;

    public ZaminUserInfoEnricher(IUserInfoService userInfoService)
    {
        this._userInfoService = userInfoService;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory factory)
    {
        string userName;
        string UserId;
        string UserIp;


        userName = _userInfoService.GetUsername();
        if (string.IsNullOrEmpty(userName))
        {
            userName = "Unknown";
        }
        UserId = _userInfoService.UserIdOrDefault();
        UserIp = _userInfoService.GetUserIp();

        var userNameProperty = factory.CreateProperty("UserName", userName);
        var userIdProperty = factory.CreateProperty("UserId", UserId);
        var userIpProperty = factory.CreateProperty("UserIp", UserIp);
        logEvent.AddPropertyIfAbsent(userNameProperty);
        logEvent.AddPropertyIfAbsent(userIdProperty);
        logEvent.AddPropertyIfAbsent(userIpProperty);
    }
}
