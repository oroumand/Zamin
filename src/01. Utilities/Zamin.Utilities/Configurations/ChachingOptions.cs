using System;
using System.Linq;

namespace Zamin.Utilities.Configurations
{
    public class CachingOptions
    {
        public bool Enable { get; set; }
        public bool EnableQueryAutomaticCache { get; set; }
        public CacheProvider Provider { get; set; }
        public DistributedSqlServerCacheOptions DistributedSqlServerCache { get; set; }
        public StackexchangeRedisCacheOptions StackExchangeRedisCache { get; set; }
        public NcacheDistributedCacheOptions NCacheDistributedCache { get; set; }
        public PolicyOptions[] Policies { get; set; }

        public PolicyOptions GetFor(string typeName) =>
             Policies?.Length < 1 ? null :
                 Policies.OrderBy(c => c.Order).FirstOrDefault(c =>
                 c?.Excludes?.Any(d => typeName.Contains(d)) == false &&
                 (c?.Includes?.Any(d => typeName.Contains(d)) == true ||
                 c?.Includes?.Contains("*") == true));

    }

    public class DistributedSqlServerCacheOptions
    {
        public string ConnectionString { get; set; }
        public string SchemaName { get; set; }
        public string TableName { get; set; }
    }

    public class StackexchangeRedisCacheOptions
    {
        public string Configuration { get; set; }
        public string SampleInstance { get; set; }
    }

    public class NcacheDistributedCacheOptions
    {
        public string CacheName { get; set; }
        public bool EnableLogs { get; set; }
        public bool ExceptionsEnabled { get; set; }
    }

    public class PolicyOptions
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public DateTime? AbsoluteExpiration { get; set; }
        public int SlidingExpiration { get; set; }
        public string[] Includes { get; set; }
        public string[] Excludes { get; set; }
    }
}
