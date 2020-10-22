using Zamin.Utilities.Services.Messaging;
using Zamin.Utilities.Services.Serializers;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using Zamin.Utilities.Extentions;
namespace Zamin.Infra.Tools.Messaging.RabbitMq
{
    public class RabbitMqMessagePublisher : IAsyncMessagePublisher
    {
        private readonly IConfiguration _configuration;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IConnection _connection;
        private readonly string _serviceId;
        private readonly string _exchangeName;
        
        public RabbitMqMessagePublisher(IConfiguration configuration, IJsonSerializer jsonSerializer)
        {
            _configuration = configuration;
            
            this._jsonSerializer = jsonSerializer;
            var connectionFactory = new ConnectionFactory();
            _serviceId = _configuration["ZaminConfigurations:ServiceId"];
            var userName = _configuration["ZaminConfigurations:Messaging:RabbitMq:UserName"];
            var password = _configuration["ZaminConfigurations:Messaging:RabbitMq:Password"];
            var host = _configuration["ZaminConfigurations:Messaging:RabbitMq:Host"];
            var port = _configuration["ZaminConfigurations:Messaging:RabbitMq:Port"];
            var protocol = _configuration["ZaminConfigurations:Messaging:RabbitMq:Protocol"];
            _exchangeName = _configuration["ZaminConfigurations:Messaging:RabbitMq:ExchangeName"];
            var exchangeDurable = bool.Parse(_configuration["ZaminConfigurations:Messaging:RabbitMq:ExchangeDurable"]);
            var exchangeAutoDeleted = bool.Parse(_configuration["ZaminConfigurations:Messaging:RabbitMq:ExchangeAutoDeleted"]);


            connectionFactory.Uri = new Uri($"{protocol}://{userName}:{password}@{host}:{port}");
            _connection = connectionFactory.CreateConnection();
            var channel = _connection.CreateModel();
            channel.ExchangeDeclare(_exchangeName, ExchangeType.Topic, exchangeDurable, exchangeAutoDeleted);
        }

        public void Publish<TInput>(TInput input)
        {
            var channel = _connection.CreateModel();
            string eventName = input.GetType().Name;
            var routingKey = $"{_serviceId}.{eventName}";
            channel.BasicPublish(_exchangeName, routingKey, null, _jsonSerializer.Serilize(input).ToByteArray());
        }

        public void Publish(string jsonInput)
        {
            throw new NotImplementedException();
        }

        public void Publish(string rout, string jsonInput)
        {
            throw new NotImplementedException();
        }

        public void Publish(string messageId, string correlationId, string eventName, string jsonInput)
        {
            var channel = _connection.CreateModel();
           
            var routingKey = $"{_serviceId}.{eventName}";
            var basicProperties = channel.CreateBasicProperties();
            basicProperties.AppId = _serviceId;
            basicProperties.CorrelationId = correlationId;
            basicProperties.MessageId = messageId;

            channel.BasicPublish(_exchangeName, routingKey, basicProperties, jsonInput.ToByteArray());
        }
    }
}
