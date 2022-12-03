namespace Zamin.Extensions.MessageBus.MessageInbox.Abstractions.Options;

public class MessageInboxOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public bool AutoCreateSqlTable { get; set; } = true;
    public string TableName { get; set; } = "ParrotTranslations";
    public string SchemaName { get; set; } = "dbo";
    public string ApplicationName { get; set; } = string.Empty;
}