using System;
using System.Collections.Generic;
using System.Text;

namespace Zamin.Utilities.Configurations
{


    public class MessageBus
    {
        public string MessageBusTypeName { get; set; }
        public string MessageConsumerTypeName { get; set; }
        public RabbitMq RabbitMq { get; set; }
    }

    public class RabbitMq
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
    public class Messageconsumer
    {
        public string MessageInboxStoreTypeName { get; set; }
        public SqlMessageInboxStore SqlMessageInboxStore { get; set; }
        public Command[] Commands { get; set; }
        public Event[] Events { get; set; }
    }
    public class SqlMessageInboxStore
    {
        public string ConnectionString { get; set; }
    }
    public class Command
    {
        public string CommandName { get; set; }
        public string MapToClass { get; set; }
    }

    public class Event
    {
        public string FromServiceId { get; set; }
        public Eventdata[] EventData { get; set; }
    }

    public class Eventdata
    {
        public string EventName { get; set; }
        public string MapToClass { get; set; }
    }
}
