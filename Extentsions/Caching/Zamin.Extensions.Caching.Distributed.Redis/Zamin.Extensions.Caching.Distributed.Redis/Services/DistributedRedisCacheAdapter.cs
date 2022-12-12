using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text;
using Zamin.Extensions.Caching.Abstractions;
using Zamin.Extensions.Serializers.Abstractions;

namespace Zamin.Extensions.Caching.Distributed.Redis.Services;

public class DistributedRedisCacheAdapter : ICacheAdapter
{
    private readonly IDistributedCache _cache;
    private readonly IJsonSerializer _serializer;
    private readonly ILogger<DistributedRedisCacheAdapter> _logger;

    public DistributedRedisCacheAdapter(IDistributedCache cache,
                                        IJsonSerializer serializer,
                                        ILogger<DistributedRedisCacheAdapter> logger)
    {
        _cache = cache;
        _serializer = serializer;
        _logger = logger;

        _logger.LogInformation("DistributedCache Redis Adapter Start working");
    }

    public void Add<TInput>(string key, TInput obj, DateTime? absoluteExpiration, TimeSpan? slidingExpiration)
    {
        _logger.LogTrace("DistributedCache Redis Adapter Cache {obj} with key : {key} " +
                         ", with data : {data} " +
                         ", with absoluteExpiration : {absoluteExpiration} " +
                         ", with slidingExpiration : {slidingExpiration}",
                         typeof(TInput),
                         key,
                         _serializer.Serialize(obj),
                         absoluteExpiration.ToString(),
                         slidingExpiration.ToString());

        var option = new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = absoluteExpiration,
            SlidingExpiration = slidingExpiration
        };

        _cache.Set(key, Encoding.UTF8.GetBytes(_serializer.Serialize(obj)), option);
    }

    public TOutput Get<TOutput>(string key)
    {
        var result = _cache.GetString(key);

        if (!string.IsNullOrWhiteSpace(result))
        {
            _logger.LogTrace("DistributedCache Redis Adapter Successful Get Cache with key : {key} and data : {data}",
                             key,
                             _serializer.Serialize(result));

            return _serializer.Deserialize<TOutput>(result);
        }
        else
        {
            _logger.LogTrace("DistributedCache Redis Adapter Failed Get Cache with key : {key}", key);

            return default;
        }
    }

    public void RemoveCache(string key)
    {
        _logger.LogTrace("DistributedCache Redis Adapter Remove Cache with key : {key}", key);

        _cache.Remove(key);
    }
}