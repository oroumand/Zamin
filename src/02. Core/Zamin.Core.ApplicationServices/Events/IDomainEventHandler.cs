using Zamin.Core.Domain.Events;
using System.Threading.Tasks;

namespace Zamin.Core.ApplicationServices.Events
{
    public interface IDomainEventHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        Task Handle(TDomainEvent Event);
    }
}
