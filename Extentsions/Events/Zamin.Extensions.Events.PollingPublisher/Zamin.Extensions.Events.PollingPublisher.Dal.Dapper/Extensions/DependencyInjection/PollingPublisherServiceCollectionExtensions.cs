using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.Events.Abstractions;
using Zamin.Extensions.Events.PollingPublisher.Dal.Dapper.DataAccess;
using Zamin.Extensions.Events.PollingPublisher.Dal.Dapper.Options;

namespace Zamin.Extensions.Events.PollingPublisher.Dal.Dapper.Extensions.DependencyInjection;

public static class PollingPublisherServiceCollectionExtensions
{
    public static IServiceCollection AddZaminPollingPublisherDalSql(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PollingPublisherDalRedisOptions>(configuration);
        AddServices(services);
        return services;
    }

    public static IServiceCollection AddZaminPollingPublisherDalSql(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddZaminPollingPublisherDalSql(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddZaminPollingPublisherDalSql(this IServiceCollection services, Action<PollingPublisherDalRedisOptions> setupAction)
    {
        services.Configure(setupAction);
        AddServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddSingleton<IOutBoxEventItemRepository, SqlOutBoxEventItemRepository>();
    }
}