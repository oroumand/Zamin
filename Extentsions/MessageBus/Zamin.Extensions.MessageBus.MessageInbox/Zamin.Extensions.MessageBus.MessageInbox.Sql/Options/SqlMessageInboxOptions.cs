namespace Zamin.Extensions.MessageBus.MessageInbox.Sql.Options;

public class SqlMessageInboxOptions
{
    public bool AutoCreateSqlTable { get; set; } = true;
    public string ConnectionString { get; set; } = string.Empty;
    public string TableName { get; set; } = "MessageInbox";
    public string SchemaName { get; set; } = "dbo";
}