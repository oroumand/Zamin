using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.Caching.InMemory.Services;
using Zamin.Extentions.Chaching.Abstractions;

namespace Zamin.Extensions.Caching.InMemory.Extensions.DependencyInjection;

public static class InMemoryCachingServiceCollectionExtensions
{
    public static IServiceCollection AddInMemoryCaching(this IServiceCollection services)
        => services.AddMemoryCache().AddTransient<ICacheAdapter, InMemoryCacheAdapter>();
}