using Zamin.Extensions.Logger.Abstractions;
using Zamin.Utilities;

namespace Zamin.Extensions.DependencyInjection;
public static class AddZaminServicesExtentions
{
    public static IServiceCollection AddZaminUntilityServices(
        this IServiceCollection services)
    {

        services.AddScoped<IScopeInformation, ScopeInformation>();
        services.AddTransient<ZaminServices>();
        return services;
    }

}
