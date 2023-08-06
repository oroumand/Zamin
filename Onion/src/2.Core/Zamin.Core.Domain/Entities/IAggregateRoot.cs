using Zamin.Core.Domain.Events;

namespace Zamin.Core.Domain.Entities
{
    public interface IAggregateRoot
    {
        void ClearEvents();
        IEnumerable<IDomainEvent> GetEvents();
    }
}