namespace Zamin.Utilities.Configurations;
public class MessageBusOptions
{
    public string MessageBusTypeName { get; set; } = "RabbitMqMessageBus";
    public string MessageConsumerTypeName { get; set; } = "IdempotentMessageConsumer";
    public RabbitMqOptions RabbitMq { get; set; } = new RabbitMqOptions();
}

public class RabbitMqOptions
{
    public string UserName { get; set; } = "guest";
    public string Password { get; set; } = "guest";
    public string Host { get; set; } = "localhost";
    public string Port { get; set; } = "5672";
    public string VirualHost { get; set; } = string.Empty;
    public string Protocol { get; set; } = "amqp";
    public string ExchangeName { get; set; } = "DefaultExchance";
    public bool ExchangeDurable { get; set; } = false;
    public bool ExchangeAutoDeleted { get; set; } = false;
    public Uri Uri => new Uri($"{Protocol}://{UserName}:{Password}@{Host}:{Port}{VirualHost}");

}

