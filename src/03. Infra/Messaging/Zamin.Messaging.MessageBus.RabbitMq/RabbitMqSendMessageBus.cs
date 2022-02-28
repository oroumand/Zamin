using Zamin.Utilities.Configurations;
using Zamin.Utilities.Services.MessageBus;
using Zamin.Utilities.Services.Serializers;
using RabbitMQ.Client;
using Microsoft.Extensions.ObjectPool;
using Zamin.Utilities.Extensions;

namespace Zamin.Messaging.MessageBus.RabbitMq;
public class RabbitMqSendMessageBus : IDisposable, ISendMessageBus, IPooledObjectPolicy<IModel>
{
    private readonly ZaminConfigurationOptions _configuration;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IConnection _connection;

    public RabbitMqSendMessageBus(ZaminConfigurationOptions configuration, IJsonSerializer jsonSerializer)
    {
        _configuration = configuration;
        _jsonSerializer = jsonSerializer;
        var connectionFactory = new ConnectionFactory
        {
            Uri = configuration.MessageBus.RabbitMq.Uri
        };
        _connection = connectionFactory.CreateConnection();
        var channel = Create();
        channel.ExchangeDeclare(configuration.MessageBus.RabbitMq.ExchangeName, ExchangeType.Topic, configuration.MessageBus.RabbitMq.ExchangeDurable, configuration.MessageBus.RabbitMq.ExchangeAutoDeleted);
    }



    public void Publish<TInput>(TInput input)
    {
        string messageName = input.GetType().Name;
        Parcel parcel = new Parcel
        {
            MessageId = Guid.NewGuid().ToString(),
            MessageBody = _jsonSerializer.Serialize(input),
            MessageName = messageName,
            Route = $"{_configuration.ServiceId}.{messageName}",
            Headers = new Dictionary<string, object>
            {
                ["AccuredOn"] = DateTime.Now.ToString(),
            }
        };
        Send(parcel);
    }

    public void SendCommandTo<TCommandData>(string destinationService, string commandName, TCommandData commandData)
    {
        Parcel parcel = new Parcel
        {
            MessageId = Guid.NewGuid().ToString(),
            MessageBody = _jsonSerializer.Serialize(commandData),
            MessageName = commandName,
            Route = $"{destinationService}.{commandName}",
        };
        Send(parcel);
    }

    public void SendCommandTo<TCommandData>(string destinationService, string commandName, string correlationId, TCommandData commandData)
    {
        Parcel parcel = new Parcel
        {
            MessageId = Guid.NewGuid().ToString(),
            CorrelationId = correlationId,
            MessageBody = _jsonSerializer.Serialize(commandData),
            MessageName = commandName,
            Route = $"{destinationService}.{commandName}",
        };
        Send(parcel);
    }

    public void Send(Parcel parcel)
    {
        var channel = _connection.CreateModel();
        var basicProperties = channel.CreateBasicProperties();
        basicProperties.AppId = _configuration.ServiceId;
        basicProperties.CorrelationId = parcel?.CorrelationId;
        basicProperties.MessageId = parcel?.MessageId;
        basicProperties.Headers = parcel.Headers;
        basicProperties.Type = parcel.MessageName;
        channel.BasicPublish(_configuration.MessageBus.RabbitMq.ExchangeName, parcel.Route, basicProperties, parcel.MessageBody.ToByteArray());
    }

    public void Dispose()
    {
        if (_connection != null && _connection.IsOpen)
        {
            _connection.Close();
        }
    }

    public IModel Create()
    {
        return _connection.CreateModel();
    }

    public bool Return(IModel obj)
    {
        if (obj.IsOpen)
        {
            return true;
        }

        obj?.Dispose();
        return false;

    }
}
