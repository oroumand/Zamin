using Microsoft.Extensions.Caching.Memory;
using Zamin.Extentions.Chaching.Abstractions;

namespace Zamin.Extensions.Caching.InMemory.Services;

public class InMemoryCacheAdapter : ICacheAdapter
{
    private readonly IMemoryCache _memoryCache;
    public InMemoryCacheAdapter(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void Add<TInput>(string key, TInput obj, DateTime? AbsoluteExpiration, TimeSpan? SlidingExpiration)
    {
        _memoryCache.Set(key, obj);
    }

    public TOutput Get<TOutput>(string key)
    {
        var result = _memoryCache.TryGetValue(key, out TOutput resultObject);
        return resultObject;
    }

    public void RemoveCache(string key)
    {
        _memoryCache.Remove(key);
    }
}
