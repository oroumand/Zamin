using Zamin.Core.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

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

        public async Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : class, IDomainEvent
        {
            using var serviceProviderScop = _serviceFactory.CreateScope();
            var handlers = serviceProviderScop.ServiceProvider.GetServices<IDomainEventHandler<TDomainEvent>>();
            foreach (var handler in handlers)
            {
                await handler.Handle(@event);
            }
        }



        #endregion

    }
}
