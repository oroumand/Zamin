using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Zamin.Extensions.MessageBus.RabbitMQ.Options;
using Zamin.Extentions.MessageBus.Abstractions;
using Zamin.Extentions.Serializers.Abstractions;
using Zamin.Utilities.Extensions;

namespace Zamin.Extensions.MessageBus.RabbitMQ
{
    public class RabbitMqSendMessageBus : IDisposable, ISendMessageBus
    {
        private readonly IModel _channel;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly RabbitMqOptions _rabbitMqOptions;

        public RabbitMqSendMessageBus(IConnection connection, IJsonSerializer jsonSerializer, IOptions<RabbitMqOptions> rabbitMqOptions)
        {
            _jsonSerializer = jsonSerializer;
            _rabbitMqOptions = rabbitMqOptions.Value;
            _channel = connection.CreateModel();
            _channel.ExchangeDeclare(_rabbitMqOptions.ExchangeName, ExchangeType.Topic, true, false, null);
        }


        public void Publish<TInput>(TInput input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            string messageName = input.GetType().Name;
            Parcel parcel = new()
            {
                MessageId = Guid.NewGuid().ToString(),
                MessageBody = _jsonSerializer.Serialize(input),
                MessageName = messageName,
                Route = $"{_rabbitMqOptions.ApplicationName}.{messageName}",
                Headers = new Dictionary<string, object>
                {
                    ["AccuredOn"] = DateTime.Now.ToString(),
                }
            };
            Send(parcel);
        }



        public void SendCommandTo<TCommandData>(string destinationService, string commandName, TCommandData commandData)
        {
            if (commandData == null)
                throw new ArgumentNullException(nameof(commandData));
            Parcel parcel = new()
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
            if (commandData == null)
                throw new ArgumentNullException(nameof(commandData));
            Parcel parcel = new()
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
            if (parcel == null)
                throw new ArgumentNullException(nameof(parcel));

            var basicProperties = _channel.CreateBasicProperties();
            basicProperties.Persistent = _rabbitMqOptions.PerssistMessage;
            basicProperties.AppId = _rabbitMqOptions.ApplicationName;
            basicProperties.CorrelationId = parcel?.CorrelationId;
            basicProperties.MessageId = parcel?.MessageId;
            basicProperties.Headers = parcel?.Headers;
            basicProperties.Type = parcel.MessageName;
            _channel.BasicPublish(_rabbitMqOptions.ExchangeName, parcel.Route, basicProperties, parcel.MessageBody.ToByteArray());
        }
        public void Dispose()
        {
            if (_channel != null)
            {
                if (_channel.IsOpen)
                    _channel.Close();
                _channel.Dispose();
            }
        }
    }
}