namespace Zamin.Extensions.ChangeDataLog.Sql.Options;

public sealed record ChangeDataLogSqlOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public bool AutoCreateSqlTable { get; set; } = true;
    public string EntityTableName { get; set; } = "EntityChageDataLogs";
    public string PropertyTableName { get; set; } = "PropertyChageDataLogs";
    public string SchemaName { get; set; } = "dbo";
}
