using Zamin.Utilities.Configurations;
using Zamin.Utilities.Services.MessageBus;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Zamin.Messaging.IdempotentConsumers
{
    public class IdempotentConsumerHostedService : IHostedService
    {
        private readonly ZaminConfigurationOptions _configuration;
        private readonly IReceiveMessageBus _receiveMessageBus;

        public IdempotentConsumerHostedService(ZaminConfigurationOptions configuration, IReceiveMessageBus receiveMessageBus)
        {
            _configuration = configuration;
            _receiveMessageBus = receiveMessageBus;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            ReveiveMessages();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void ReveiveMessages()
        {
            if (_configuration?.Messageconsumer?.Commands?.Any() == true)
            {
                foreach (var item in _configuration.Messageconsumer.Commands.ToList())
                {
                    _receiveMessageBus.Receive(item.CommandName);
                }
            }

            if (_configuration?.Messageconsumer?.Events?.Any() == true)
            {
                foreach (var eventPublisher in _configuration.Messageconsumer.Events.ToList())
                {
                    foreach (var @event in eventPublisher?.EventData)
                    {
                        _receiveMessageBus.Subscribe(eventPublisher.FromServiceId, @event.EventName);
                    }
                }
            }


        }
    }
}
