using System;

namespace Zamin.Utilities.Configurations
{


    public class MessageBusOptions
    {
        public string MessageBusTypeName { get; set; }
        public string MessageConsumerTypeName { get; set; }
        public RabbitMqOptions RabbitMq { get; set; }
    }

    public class RabbitMqOptions
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string VirualHost { get; set; }
        public string Protocol { get; set; }
        public string ExchangeName { get; set; }
        public bool ExchangeDurable { get; set; }
        public bool ExchangeAutoDeleted { get; set; }
        public Uri Uri => new Uri($"{Protocol}://{UserName}:{Password}@{Host}:{Port}{VirualHost}");

    }
    public class MessageConsumerOptions
    {
        public string MessageInboxStoreTypeName { get; set; }
        public SqlMessageInboxStoreOptions SqlMessageInboxStore { get; set; }
        public CommandOptions[] Commands { get; set; }
        public EventOptions[] Events { get; set; }
    }
    public class SqlMessageInboxStoreOptions
    {
        public string ConnectionString { get; set; }
    }
    public class CommandOptions
    {
        public string CommandName { get; set; }
        public string MapToClass { get; set; }
    }

    public class EventOptions
    {
        public string FromServiceId { get; set; }
        public EventDataOptions[] EventData { get; set; }
    }

    public class EventDataOptions
    {
        public string EventName { get; set; }
        public string MapToClass { get; set; }
    }
}
