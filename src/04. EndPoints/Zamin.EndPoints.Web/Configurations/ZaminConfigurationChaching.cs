using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zamin.EndPoints.Web.Configurations
{
    public enum ChachProvider
    {
        MemoryCache,
        DistributedSqlServerCache,
        StackExchangeRedisCache,
        NCacheDistributedCache
    }
    public class Chaching
    {
        public bool Enable { get; set; }
        public bool EnableQueryAutomaticCache { get; set; }
        public ChachProvider Provider { get; set; }
        public Distributedsqlservercache DistributedSqlServerCache { get; set; }
        public Stackexchangerediscache StackExchangeRedisCache { get; set; }
        public Ncachedistributedcache NCacheDistributedCache { get; set; }
        public Policy[] Policies { get; set; }

        public Policy GetFor(string typeName) =>
             Policies?.Length < 1 ? null :
                 Policies.OrderBy(c => c.Order).FirstOrDefault(c =>
                 c?.Excludes?.Any(d => typeName.Contains(d)) == false &&
                 (c?.Includes?.Any(d => typeName.Contains(d)) == true ||
                 c?.Includes?.Contains("*") == true));

    }

    public class Distributedsqlservercache
    {
        public string ConnectionString { get; set; }
        public string SchemaName { get; set; }
        public string TableName { get; set; }
    }

    public class Stackexchangerediscache
    {
        public string Configuration { get; set; }
        public string SampleInstance { get; set; }
    }

    public class Ncachedistributedcache
    {
        public string CacheName { get; set; }
        public bool EnableLogs { get; set; }
        public bool ExceptionsEnabled { get; set; }
    }

    public class Policy
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public DateTime? AbsoluteExpiration { get; set; }
        public int SlidingExpiration { get; set; }
        public string[] Includes { get; set; }
        public string[] Excludes { get; set; }
    }
}
