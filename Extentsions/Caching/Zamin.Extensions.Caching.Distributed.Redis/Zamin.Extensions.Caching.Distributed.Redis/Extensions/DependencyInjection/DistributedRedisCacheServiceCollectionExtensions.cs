using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.Caching.Abstractions;
using Zamin.Extensions.Caching.Distributed.Redis.Options;
using Zamin.Extensions.Caching.Distributed.Redis.Services;

namespace Zamin.Extensions.DependencyInjection;

public static class DistributedRedisCacheServiceCollectionExtensions
{
    public static IServiceCollection AddZaminRedisDistributedCache(this IServiceCollection services,
                                                                   IConfiguration configuration,
                                                                   string sectionName)
        => services.AddZaminRedisDistributedCache(configuration.GetSection(sectionName));

    public static IServiceCollection AddZaminRedisDistributedCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ICacheAdapter, DistributedRedisCacheAdapter>();
        services.Configure<DistributedRedisCacheOptions>(configuration);

        var option = configuration.Get<DistributedRedisCacheOptions>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = option.Configuration;
            options.InstanceName = option.InstanceName;
        });

        return services;
    }

    public static IServiceCollection AddZaminRedisDistributedCache(this IServiceCollection services,
                                                              Action<DistributedRedisCacheOptions> setupAction)
    {
        services.AddTransient<ICacheAdapter, DistributedRedisCacheAdapter>();
        services.Configure(setupAction);

        var option = new DistributedRedisCacheOptions();
        setupAction.Invoke(option);

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = option.Configuration;
            options.InstanceName = option.InstanceName;
        });

        return services;
    }
}