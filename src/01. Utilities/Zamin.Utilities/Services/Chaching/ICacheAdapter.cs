using System;

namespace Zamin.Utilities.Services.Chaching
{
    public interface ICacheAdapter
    {
        void Add<TInput>(string key, TInput obj, DateTime? AbsoluteExpiration, TimeSpan? SlidingExpiration);
        TOutput Get<TOutput>(string key);
        void RemoveCache(string key);
    }
}
