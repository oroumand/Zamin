using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Zamin.Core.Domain.Events;

namespace Zamin.Core.ApplicationServices.Events
{

    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceScopeFactory _serviceFactory;
        public EventDispatcher(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceFactory = serviceScopeFactory;
        }
        #region Event Dispatcher
        public async Task PublihAsync<TDomianEvent>(TDomianEvent @event) where TDomianEvent : class, IDomainEvent
        {
            using var serviceProviderScop = _serviceFactory.CreateScope();
            var handlers = serviceProviderScop.ServiceProvider.GetServices<IDomainEventHandler<TDomianEvent>>();
            foreach (var handler in handlers)
            {
                await handler.Handle(@event);
            }
        }

        public async Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : class, IDomainEvent
        {
            using var serviceProviderScop = _serviceFactory.CreateScope();
            var handlers = serviceProviderScop.ServiceProvider.GetServices<IDomainEventHandler<TDomainEvent>>();
            foreach (var handler in handlers)
            {
                await handler.Handle(@event);
            }
        }

        public async Task PublishIntegrationEventAsync<TIntegrationEvent>(TIntegrationEvent @event) where TIntegrationEvent : class, IIntegrationEvent
        {
            using var serviceProviderScop = _serviceFactory.CreateScope();
            var handlers = serviceProviderScop.ServiceProvider.GetServices<IIntegrationEventHandler<TIntegrationEvent>>();
            foreach (var handler in handlers)
            {
                await handler.Handle(@event);
            }
        }

        #endregion

    }
}
