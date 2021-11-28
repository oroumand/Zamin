namespace Zamin.Utilities.Services.Chaching;

public class FakeCacheAdapter : ICacheAdapter
{
    public void Add<TInput>(string key, TInput obj, DateTime? AbsoluteExpiration, TimeSpan? SlidingExpiration) { }

    public TOutput Get<TOutput>(string key) => default;


    public void RemoveCache(string key) { }
}
