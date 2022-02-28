using Zamin.Utilities.Services.Chaching;
using Zamin.Utilities.Services.Serializers;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Zamin.Infra.Tools.Caching.Microsoft;
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
        _cache.Set("", Encoding.UTF8.GetBytes(_serializer.Serialize(obj)), new DistributedCacheEntryOptions
        {

        });
        _cache.Set(key, Encoding.UTF8.GetBytes(_serializer.Serialize(obj)));
    }

    public void Add<TInput>(string key, TInput obj, DateTime? AbsoluteExpiration, TimeSpan? SlidingExpiration)
    {
        _cache.Set(key, Encoding.UTF8.GetBytes(_serializer.Serialize(obj)), new DistributedCacheEntryOptions
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
        throw new NotImplementedException();
    }
}
