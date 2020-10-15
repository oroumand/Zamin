using Zamin.Core.Domain.Events;
using System.Threading.Tasks;

namespace Zamin.Core.ApplicationServices.Events
{
    public interface IEventDispatcher
    {
        Task PublishIntegrationEventAsync<TIntegrationEvent>(TIntegrationEvent @event) where TIntegrationEvent : class, IIntegrationEvent;
        Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : class, IDomainEvent;

    }
}
