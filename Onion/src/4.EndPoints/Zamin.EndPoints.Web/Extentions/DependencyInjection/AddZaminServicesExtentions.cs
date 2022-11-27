using Zamin.Utilities;

namespace Zamin.Extensions.DependencyInjection;
public static class AddZaminServicesExtentions
{
    public static IServiceCollection AddZaminUntilityServices(
        this IServiceCollection services)
    {
        services.AddTransient<ZaminServices>();
        return services;
    }
}