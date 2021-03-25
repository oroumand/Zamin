using Zamin.Utilities.Configurations;
using Zamin.Utilities.Services.MessageBus;
using Zamin.Utilities.Services.Serializers;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using Zamin.Utilities.Extentions;
using RabbitMQ.Client.Events;
using System.Linq;

namespace Zamin.Messaging.MessageBus.RabbitMq
{
    public class RabbitMqMessageBus : IMessageBus
    {
        private readonly ZaminConfigurationOptions _configuration;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IMessageConsumer _messageConsumer;
        private readonly IConnection _connection;

        public RabbitMqMessageBus(ZaminConfigurationOptions configuration, IJsonSerializer jsonSerializer, IMessageConsumer messageConsumer)
        {
            _configuration = configuration;
            _jsonSerializer = jsonSerializer;
            _messageConsumer = messageConsumer;
            var connectionFactory = new ConnectionFactory
            {
                Uri = configuration.MessageBus.RabbitMq.Uri
            };
            _connection = connectionFactory.CreateConnection();
            var channel = _connection.CreateModel();
            channel.ExchangeDeclare(configuration.MessageBus.RabbitMq.ExchangeName, ExchangeType.Topic, configuration.MessageBus.RabbitMq.ExchangeDurable, configuration.MessageBus.RabbitMq.ExchangeAutoDeleted);
            ReveiveMessages();
        }

        private void ReveiveMessages()
        {
            if (_configuration?.Messageconsumer?.Commands?.Any() == true)
            {
                foreach (var item in _configuration.Messageconsumer.Commands.ToList())
                {
                    Receive(item.CommandName);
                }
            }

            if (_configuration?.Messageconsumer?.Events?.Any() == true)
            {
                foreach (var eventPublisher in _configuration.Messageconsumer.Events.ToList())
                {
                    foreach (var @event in eventPublisher?.EventData)
                    {
                        Subscribe(eventPublisher.FromServiceId, @event.EventName);
                    }
                }
            }
        }

        public void Publish<TInput>(TInput input)
        {
            string messageName = input.GetType().Name;
            Parcel parcel = new Parcel
            {
                MessageId = Guid.NewGuid().ToString(),
                MessageBody = _jsonSerializer.Serilize(input),
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
                MessageBody = _jsonSerializer.Serilize(commandData),
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
                MessageBody = _jsonSerializer.Serilize(commandData),
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

        public void Subscribe(string serviceId, string eventName)
        {
            var queueName = $"{serviceId}.{eventName}";
            MessageReceiver(queueName, Consumer_EventReceived);
        }

        public void Receive(string commandName)
        {
            var queueName = $"{_configuration.ServiceId}.{commandName}";
            MessageReceiver(queueName, Consumer_CommandReceived);
        }

        private void MessageReceiver(string route, EventHandler<BasicDeliverEventArgs> eventHandler)
        {
            var channel = _connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += eventHandler;
            var queue = channel.QueueDeclare($"{ _configuration.ServiceId}", true, false, false);

            channel.QueueBind(queue.QueueName, _configuration.MessageBus.RabbitMq.ExchangeName, route);
            channel.BasicConsume(queue.QueueName, true, consumer);
        }
        private void Consumer_EventReceived(object sender, BasicDeliverEventArgs e)
        {
            _messageConsumer.ConsumeEvent(e.BasicProperties.AppId, e.ToParcel());
        }

        private void Consumer_CommandReceived(object sender, BasicDeliverEventArgs e)
        {
            _messageConsumer.ConsumeCommand(e.BasicProperties.AppId, e.ToParcel());

        }
    }
}
