using Zamin.Utilities.Configurations;
using Zamin.Utilities.Services.MessageBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Zamin.Messaging.MessageBus.RabbitMq;
public class RabbitMqReceiveMessageBus : IReceiveMessageBus, IDisposable
{
    private readonly ILogger<RabbitMqReceiveMessageBus> _logger;
    private readonly ZaminConfigurationOptions _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    private IServiceScopeFactory _serviceScopeFactory;

    public RabbitMqReceiveMessageBus(ILogger<RabbitMqReceiveMessageBus> logger, ZaminConfigurationOptions configuration, IServiceScopeFactory serviceScopeFactory, IMessageConsumer messageConsumer)
    {
        _logger = logger;
        _configuration = configuration;
        _serviceScopeFactory = serviceScopeFactory;
        var connectionFactory = new ConnectionFactory
        {
            Uri = configuration.MessageBus.RabbitMq.Uri
        };
        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(configuration.MessageBus.RabbitMq.ExchangeName, ExchangeType.Topic, configuration.MessageBus.RabbitMq.ExchangeDurable, configuration.MessageBus.RabbitMq.ExchangeAutoDeleted);
    }



    public void Subscribe(string serviceId, string eventName)
    {
        var route = $"{serviceId}.{eventName}";

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += Consumer_EventReceived;
        var queue = _channel.QueueDeclare($"{_configuration.ServiceId}.EventsInput", true, false, false);
        _channel.QueueBind(queue.QueueName, _configuration.MessageBus.RabbitMq.ExchangeName, route);
        _channel.BasicConsume(queue.QueueName, true, consumer);
    }

    public void Receive(string commandName)
    {
        var route = $"{_configuration.ServiceId}.{commandName}";

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += Consumer_CommandReceived;
        var queue = _channel.QueueDeclare($"{_configuration.ServiceId}.CommandsInput", true, false, false);

        _channel.QueueBind(queue.QueueName, _configuration.MessageBus.RabbitMq.ExchangeName, route);
        _channel.BasicConsume(queue.QueueName, true, consumer);
    }

    private void Consumer_EventReceived(object sender, BasicDeliverEventArgs e)
    {
        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            try
            {
                var consumer = scope.ServiceProvider.GetRequiredService<IMessageConsumer>();
                consumer.ConsumeEvent(e.BasicProperties.AppId, e.ToParcel());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }
    }

    private void Consumer_CommandReceived(object sender, BasicDeliverEventArgs e)
    {
        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            try
            {
                var consumer = scope.ServiceProvider.GetRequiredService<IMessageConsumer>();
                consumer.ConsumeCommand(e.BasicProperties.AppId, e.ToParcel());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}

