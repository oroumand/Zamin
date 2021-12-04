using Zamin.Core.Domain.Events;

namespace Zamin.Core.ApplicationServices.Events;
public interface IEventDispatcher
{
    Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : class, IDomainEvent;

}

