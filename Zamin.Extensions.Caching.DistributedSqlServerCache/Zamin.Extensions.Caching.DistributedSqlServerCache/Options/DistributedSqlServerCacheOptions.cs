namespace Zamin.Extensions.Caching.DistributedSqlServerCache.Options;

public class DistributedSqlServerCacheOptions
{
    public bool AutoCreateSqlTable { get; set; } = true;
    public string ConnectionString { get; set; } = string.Empty;
    public string SchemaName { get; set; } = "dbo";
    public string TableName { get; set; } = "TableName";
}
