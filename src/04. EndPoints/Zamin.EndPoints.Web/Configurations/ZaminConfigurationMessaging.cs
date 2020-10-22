using System;
using System.Collections.Generic;
using System.Text;

namespace Zamin.EndPoints.Web.Configurations
{
    public class Messaging
    {
        public string MessageBusTypeName { get; set; }
        public EventOutbox EventOutbox { get; set; }
        public Rabbitmq RabbitMq { get; set; }
        public Inputs Inputs { get; set; }
    }

    public class Rabbitmq
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Protocol { get; set; }
        public string ExchangeName { get; set; }
        public bool ExchangeDurable { get; set; }
        public bool ExchangeAutoDeleted { get; set; }
    }

    public class EventOutbox
    {
        public bool Enabled { get; set; }
        public string ConnectionString { get; set; }
        public string OutBoxRepositoryTypeName { get; set; }
        public int SendOutBoxInterval { get; set; }
        public int SendOutBoxCount { get; set; }
    }
   
    public class Inputs
    {
        public object[] Commands { get; set; }
        public Eventinbox[] EventInbox { get; set; }
    }

    public class Eventinbox
    {
        public string ServiceId { get; set; }
        public Event[] Events { get; set; }
    }

    public class Event
    {
        public string Name { get; set; }
        public string MapToClass { get; set; }
    }

}
