using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using Zamin.Extensions.MessageBus.RabbitMQ.Extensions;
using Zamin.Extensions.MessageBus.RabbitMQ.Options;
using Zamin.Extentions.MessageBus.Abstractions;

namespace Zamin.Extensions.MessageBus.RabbitMQ;
public class RabbitMqReceiveMessageBus : IReceiveMessageBus, IDisposable
{
    private readonly ILogger<RabbitMqReceiveMessageBus> _logger;
    private readonly RabbitMqOptions _rabbitMqOptions;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _eventQueueName;
    private readonly string _commandQueueName;
    private IServiceScopeFactory _serviceScopeFactory;

    public RabbitMqReceiveMessageBus(IConnection connection,
                                     ILogger<RabbitMqReceiveMessageBus> logger,
                                     IOptions<RabbitMqOptions> rabbitMqOptions,
                                     IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _rabbitMqOptions = rabbitMqOptions.Value;
        _serviceScopeFactory = serviceScopeFactory;
        _connection = connection;
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(_rabbitMqOptions.ExchangeName, ExchangeType.Topic, true, false, null);
        _commandQueueName = $"{_rabbitMqOptions.ServiceName}.CommandsInputQueue";
        _eventQueueName = $"{_rabbitMqOptions.ServiceName}.EventsInputQueue";
        CreateCommandQueue();
        CreateEventQueue();
    }

    #region Public methods
    public void Subscribe(string serviceId, string eventName)
    {
        var route = $"{serviceId}.{RabbitMqSendMessageBusConstants.@event}.{eventName}";
        _channel.QueueBind(_eventQueueName, _rabbitMqOptions.ExchangeName, route);
        _logger.LogInformation("ServiceId: {serviceId} With EventName: {eventName} Binded.", serviceId, eventName);
    }

    public void Receive(string commandName)
    {
        var route = $"{_rabbitMqOptions.ServiceName}.{RabbitMqSendMessageBusConstants.command}.{commandName}";
        _channel.QueueBind(_commandQueueName, _rabbitMqOptions.ExchangeName, route);
        _logger.LogInformation("Command With CommandName: {commandName} Binded.", commandName);
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
    #endregion

    #region Private methods
    private void CreateEventQueue()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += Consumer_EventReceived;
        var queue = _channel.QueueDeclare(_eventQueueName, true, false, false);
        _channel.BasicConsume(queue.QueueName, false, consumer);
        _logger.LogInformation("Event Queue With Name {queueName} Created.", queue.QueueName);
    }

    private async void Consumer_EventReceived(object sender, BasicDeliverEventArgs e)
    {
        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            try
            {
                var consumer = scope.ServiceProvider.GetRequiredService<IMessageConsumer>();
                var received = await consumer.ConsumeEventAsync(e.BasicProperties.AppId, e.ToParcel());
                if (received)
                    _channel.BasicAck(e.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }
        var message = e;
        _logger.LogDebug("Event Received With RoutingKey: {RoutingKey}.", e.RoutingKey);
    }

    private void CreateCommandQueue()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += Consumer_CommandReceived;
        var queue = _channel.QueueDeclare(_commandQueueName, true, false, false);
        _channel.BasicConsume(queue.QueueName, false, consumer);
        _logger.LogInformation("Command Queue With Name {commandName} Created.", queue.QueueName);
    }

    private async void Consumer_CommandReceived(object sender, BasicDeliverEventArgs e)
    {
        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            try
            {
                Activity span = StartChildActivity(e);
                var consumer = scope.ServiceProvider.GetRequiredService<IMessageConsumer>();
                var received = await consumer.ConsumeCommandAsync(e.BasicProperties.AppId, e.ToParcel());
                if (received)
                    _channel.BasicAck(e.DeliveryTag,false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }
        var message = e;
        _logger.LogDebug("Command Received With RoutingKey: {RoutingKey}.", e.RoutingKey);

    }

    private Activity StartChildActivity(BasicDeliverEventArgs e)
    {
        var span = new Activity("RabbitMqCommandReceived");
        span.AddTag("ApplicationName", _rabbitMqOptions.ServiceName);
        if (e.BasicProperties != null && e.BasicProperties.Headers != null && e.BasicProperties.Headers.ContainsKey("TraceId") && e.BasicProperties.Headers.ContainsKey("SpanId"))
        {
            span.SetParentId(ActivityTraceId.CreateFromBytes(System.Text.Encoding.UTF8.GetBytes(e.BasicProperties.Headers["TraceId"].ToString())), ActivitySpanId.CreateFromBytes(System.Text.Encoding.UTF8.GetBytes(e.BasicProperties.Headers["SpanId"].ToString())));
        }
        span.Start();
        return span;
    }
    #endregion
}