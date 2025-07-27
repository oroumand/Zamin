namespace Zamin.Extensions.MessageBus.RabbitMQ.Options
{
    public class RabbitMqOptions
    {
        public string Url { get; set; }
        public bool PerssistMessage { get; set; }
        public string ExchangeName { get; set; }
        public string ServiceName { get; set; }
        public bool IsMessageMandatory { get; set; } = false;
    }
}
