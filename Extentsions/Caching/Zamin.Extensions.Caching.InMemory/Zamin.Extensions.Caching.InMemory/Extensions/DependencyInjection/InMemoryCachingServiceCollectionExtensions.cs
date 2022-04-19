using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.Caching.Abstractions;
using Zamin.Extensions.Caching.InMemory.Services;

namespace Zamin.Extensions.DependencyInjection;

public static class InMemoryCachingServiceCollectionExtensions
{
    public static IServiceCollection AddZaminInMemoryCaching(this IServiceCollection services)
        => services.AddMemoryCache().AddTransient<ICacheAdapter, InMemoryCacheAdapter>();
}