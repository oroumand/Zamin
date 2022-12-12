using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Diagnostics;
using Zamin.Extensions.MessageBus.RabbitMQ.Options;
using Zamin.Extensions.MessageBus.Abstractions;
using Zamin.Extensions.Serializers.Abstractions;
using Zamin.Utilities.Extensions;

namespace Zamin.Extensions.MessageBus.RabbitMQ
{
    public class RabbitMqSendMessageBus : IDisposable, ISendMessageBus
    {
        #region Fields And Properties
        private readonly IModel _channel;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ILogger<RabbitMqSendMessageBus> _logger;
        private readonly RabbitMqOptions _rabbitMqOptions;
        #endregion

        #region Constructors

        public RabbitMqSendMessageBus(IConnection connection, IJsonSerializer jsonSerializer, IOptions<RabbitMqOptions> rabbitMqOptions,ILogger<RabbitMqSendMessageBus> logger)
        {
            _jsonSerializer = jsonSerializer;
            _logger = logger;
            _rabbitMqOptions = rabbitMqOptions.Value;
            _channel = connection.CreateModel();
            _channel.ExchangeDeclare(_rabbitMqOptions.ExchangeName, ExchangeType.Topic, true, false, null);
        }

        #endregion

        #region Public methods
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
                Route = $"{_rabbitMqOptions.ServiceName}.{RabbitMqSendMessageBusConstants.@event}.{messageName}",
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
                Route = $"{destinationService}.{RabbitMqSendMessageBusConstants.command}.{commandName}"
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
                Route = $"{destinationService}.{RabbitMqSendMessageBusConstants.command}.{commandName}"
            };
            Send(parcel);
        }
        public void Send(Parcel parcel)
        {
            if (parcel is null)
                throw new ArgumentNullException(nameof(parcel));
            Activity childActivity = StartChildActivity(parcel);
            AddActivityHeaders(parcel, childActivity);

            var basicProperties = _channel.CreateBasicProperties();

            basicProperties.Persistent = _rabbitMqOptions.PerssistMessage;
            basicProperties.AppId = _rabbitMqOptions.ServiceName;
            basicProperties.CorrelationId = parcel?.CorrelationId;
            basicProperties.MessageId = parcel?.MessageId;
            basicProperties.Headers = parcel?.Headers;
            basicProperties.Type = parcel.MessageName;
            _channel.BasicPublish(_rabbitMqOptions.ExchangeName, parcel.Route, basicProperties, parcel.MessageBody.ToByteArray());
            _logger.LogDebug("Message Sent {MessageName}",parcel.MessageName);
        }
        #endregion

        #region Private methods
        private static void AddActivityHeaders(Parcel parcel, Activity childActivity)
        {
            if (parcel.Headers is null)
            {
                parcel.Headers = new Dictionary<string, object>();
            }
            parcel.Headers["TraceId"] = childActivity.TraceId.ToHexString();
            parcel.Headers["SpanId"] = childActivity.SpanId.ToHexString();
        }

        private Activity StartChildActivity(Parcel parcel)
        {
            var child = new Activity("SendParcel");
            child.AddTag("ParcelName", parcel.MessageName);
            child.AddTag("ApplicationName", _rabbitMqOptions.ServiceName);
            child.Start();
            return child;
        } 
        #endregion
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