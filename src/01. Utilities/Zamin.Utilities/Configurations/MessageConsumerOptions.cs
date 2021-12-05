namespace Zamin.Utilities.Configurations;
public class MessageConsumerOptions
{
    public string MessageInboxStoreTypeName { get; set; } = "SqlMessageInboxItemRepository";
    public SqlMessageInboxStoreOptions SqlMessageInboxStore { get; set; } = new SqlMessageInboxStoreOptions();
    public CommandOptions[] Commands { get; set; } = Array.Empty<CommandOptions>();
    public EventOptions[] Events { get; set; } = Array.Empty<EventOptions>();
}


public class SqlMessageInboxStoreOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public string TableName { get; set; }
    public string SchemaName { get; set; }
    public bool AutoCreateSqlTable { get; set; }
}
public class CommandOptions
{
    public string CommandName { get; set; }
    public string MapToClass { get; set; }
}

public class EventOptions
{
    public string FromServiceId { get; set; }
    public EventDataOptions[] EventData { get; set; } = Array.Empty<EventDataOptions>();
}

public class EventDataOptions
{
    public string EventName { get; set; }
    public string MapToClass { get; set; }
}

