using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Zamin.Extentions.Chaching.Abstractions;
using Zamin.Extentions.Serializers.Abstractions;

namespace Zamin.Extensions.Caching.StackExchangeRedisCache.Services;

public class DistributedCacheAdapter : ICacheAdapter
{
    private readonly IDistributedCache _cache;
    private readonly IJsonSerializer _serializer;

    public DistributedCacheAdapter(IDistributedCache distributedCache, IJsonSerializer serializer)
    {
        _cache = distributedCache;
        _serializer = serializer;
    }
    public void Add<TInput>(string key, TInput obj)
    {
        _cache.Set("", Encoding.UTF8.GetBytes(_serializer.Serilize(obj)), new DistributedCacheEntryOptions
        {

        });
        _cache.Set(key, Encoding.UTF8.GetBytes(_serializer.Serilize(obj)));
    }

    public void Add<TInput>(string key, TInput obj, DateTime? AbsoluteExpiration, TimeSpan? SlidingExpiration)
    {
        _cache.Set(key, Encoding.UTF8.GetBytes(_serializer.Serilize(obj)), new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = AbsoluteExpiration,
            SlidingExpiration = SlidingExpiration
        });
    }

    public TOutput Get<TOutput>(string key)
    {
        var result = _cache.GetString(key);
        return string.IsNullOrWhiteSpace(result) ?
            default : _serializer.Deserialize<TOutput>(result);
    }

    public void RemoveCache(string Key)
    {
        _cache.Remove(Key);
    }
}
