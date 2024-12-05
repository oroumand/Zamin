namespace Zamin.Extensions.Caching.Distributed.Sql.Options;

public class DistributedSqlCacheOptions
{
    public bool AutoCreateTable { get; set; } = true;
    public string ConnectionString { get; set; } = string.Empty;
    public string SchemaName { get; set; } = "zamin";
    public string TableName { get; set; } = "Cache";
}