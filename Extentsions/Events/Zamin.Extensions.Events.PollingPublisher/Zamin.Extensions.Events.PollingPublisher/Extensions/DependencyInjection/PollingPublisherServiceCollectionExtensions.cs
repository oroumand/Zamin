using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.Events.PollingPublisher;
using Zamin.Extensions.Events.PollingPublisher.DataAccess;
using Zamin.Extensions.Events.PollingPublisher.Options;

namespace Zamin.Extensions.DependencyInjection;

public static class PollingPublisherServiceCollectionExtensions
{
    public static IServiceCollection AddZaminPollingPublisher(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PollingPublisherOptions>(configuration);
        AddServices(services);
        return services;
    }

    public static IServiceCollection AddZaminPollingPublisher(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddZaminPollingPublisher(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddZaminPollingPublisher(this IServiceCollection services, Action<PollingPublisherOptions> setupAction)
    {
        services.Configure(setupAction);
        AddServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddSingleton<IOutBoxEventItemRepository, SqlOutBoxEventItemRepository>();

        services.AddHostedService<PoolingPublisherBackgroundService>();
    }
}