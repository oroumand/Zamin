using Zamin.Core.Domain.Events;
using System.Threading.Tasks;

namespace Zamin.Core.ApplicationServices.Events
{
    public interface IEventDispatcher
    {
        Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : class, IDomainEvent;

    }
}
