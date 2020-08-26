using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Zamin.Core.Domain.TacticalPatterns.EventSourcing
{
    public interface IEventStoreRepository
    {
        Task SaveAsync(Guid aggregateId, string aggregateName, int originatingVersion, IReadOnlyCollection<IDomainEvent> events);

        Task<IReadOnlyCollection<IDomainEvent>> LoadAsync(Guid aggregateRootId, string aggregateName);

        Task<IReadOnlyCollection<EventStoreItem>> GetAll(DateTime? afterDateTime);
    }
}
