using Zamin.Core.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Zamin.Core.ApplicationServices.Events
{

    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        #region Event Dispatcher
        public async Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : class, IDomainEvent
        {
            var handlers = _serviceProvider.GetServices<IDomainEventHandler<TDomainEvent>>();
            foreach (var handler in handlers)
            {
                await handler.Handle(@event);
            }
        }
        #endregion

    }
}
