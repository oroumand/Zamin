using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.Caching.Abstractions;
using Zamin.Extensions.Caching.Distributed.Redis.Options;
using Zamin.Extensions.Caching.Distributed.Redis.Services;

namespace Zamin.Extensions.DependencyInjection;

public static class DistributedRedisCacheServiceCollectionExtensions
{
    public static IServiceCollection AddZaminRedisDistributedCache(this IServiceCollection services, IConfiguration configuration, string sectionName)
        => services.AddZaminRedisDistributedCache(configuration.GetSection(sectionName));

    public static IServiceCollection AddZaminRedisDistributedCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DistributedRedisCacheOptions>(configuration);

        DistributedRedisCacheOptions options = configuration.Get<DistributedRedisCacheOptions>()
            ?? throw new ArgumentNullException(nameof(options));

        return services.AddServices(options);
    }

    public static IServiceCollection AddZaminRedisDistributedCache(this IServiceCollection services, Action<DistributedRedisCacheOptions> setupAction)
    {
        services.Configure(setupAction);
        DistributedRedisCacheOptions options = new();
        setupAction.Invoke(options);

        return services.AddServices(options);
    }

    private static IServiceCollection AddServices(this IServiceCollection services, DistributedRedisCacheOptions options)
    {
        services.AddTransient<ICacheAdapter, DistributedRedisCacheAdapter>();
        
        services.AddStackExchangeRedisCache(configurations =>
        {
            configurations.Configuration = options.Configuration;
            configurations.InstanceName = options.InstanceName;
        });

        return services;
    }
}