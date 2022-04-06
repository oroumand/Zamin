using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.Caching.StackExchangeRedisCache.Options;
using Zamin.Extensions.Caching.StackExchangeRedisCache.Services;
using Zamin.Extentions.Chaching.Abstractions;

namespace Zamin.Extensions.Caching.StackExchangeRedisCache.Extensions.DependencyInjection;

public static class StackexchangeRedisCacheServiceCollectionExtensions
{
    public static IServiceCollection AddRedisStackexchangeCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ICacheAdapter, DistributedCacheAdapter>();
        services.Configure<StackexchangeRedisCacheOptions>(configuration);

        var config = new StackexchangeRedisCacheOptions();
        configuration.Bind(config);

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = config.Configuration;
            options.InstanceName = config.SampleInstance;
        });
        return services;
    }

    public static IServiceCollection AddRedisStackexchangeCache(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddRedisStackexchangeCache(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddRedisStackexchangeCache(this IServiceCollection services, Action<StackexchangeRedisCacheOptions> setupAction)
    {
        services.AddTransient<ICacheAdapter, DistributedCacheAdapter>();
        services.Configure(setupAction);

        var config = new StackexchangeRedisCacheOptions();
        setupAction.Invoke(config);

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = config.Configuration;
            options.InstanceName = config.SampleInstance;
        });
        return services;
    }
}
