namespace Zamin.Utilities.Configurations;

public class CachingOptions
{
    public bool Enable { get; set; } = true;
    public bool EnableQueryAutomaticCache { get; set; } = true;
    public CacheProvider Provider { get; set; } = CacheProvider.MemoryCache;
    public DistributedSqlServerCacheOptions DistributedSqlServerCache { get; set; } = null;
    public StackexchangeRedisCacheOptions StackExchangeRedisCache { get; set; } = null;
    public NcacheDistributedCacheOptions NCacheDistributedCache { get; set; } = null;
    public PolicyOptions[] Policies { get; set; } = Array.Empty<PolicyOptions>();

    public PolicyOptions GetFor(string typeName) 
        => Policies?.Length < 1 ? 
        null 
        : 
        Policies.OrderBy(c => c.Order).FirstOrDefault(
            c => c?.Excludes?.Any(d => typeName.Contains(d)) == false 
            && (c?.Includes?.Any(d => typeName.Contains(d)) == true 
            || c?.Includes?.Contains("*") == true));
}

public class DistributedSqlServerCacheOptions
{
    public bool AutoCreateSqlTable { get; set; } = true;
    public string ConnectionString { get; set; } = string.Empty;
    public string SchemaName { get; set; } = "dbo";
    public string TableName { get; set; } = "TableName";
}

public class StackexchangeRedisCacheOptions
{
    public string Configuration { get; set; } = "localhost";
    public string SampleInstance { get; set; } = "SampleInstance";
}

public class NcacheDistributedCacheOptions
{
    public string CacheName { get; set; } = "demoClusteredCache";
    public bool EnableLogs { get; set; } = true;
    public bool ExceptionsEnabled { get; set; } = true;
}

public class PolicyOptions
{
    public string Name { get; set; } = "Default";
    public int Order { get; set; } = 1;
    public DateTime? AbsoluteExpiration { get; set; } = null;
    public int SlidingExpiration { get; set; } = 60;
    public string[] Includes { get; set; } = new string[] { "*" };
    public string[] Excludes { get; set; } = new string[] { "-" };
}

