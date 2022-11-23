using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace Zamin.Utilities.SerilogRegistration.Enrichers;

public class ZaminUserInfoEnricher : ILogEventEnricher
{
    readonly IHttpContextAccessor _httpContextAccessor;
    public ZaminUserInfoEnricher(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory factory)
    {
        string userName;
        string UserId;


        userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Unknown";
        UserId = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(a => a.Type == "sub")?.Value ?? "Unknown";

        var userNameProperty = factory.CreateProperty("UserName", userName);
        var userIdProperty = factory.CreateProperty("UserId", UserId);
        logEvent.AddPropertyIfAbsent(userNameProperty);
        logEvent.AddPropertyIfAbsent(userIdProperty);
    }
}
