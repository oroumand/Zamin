using Zamin.Core.Domain.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zamin.Core.Domain.Data
{
    public interface IDomainEventStore
    {
        void Save<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events) where TEvent : IDomainEvent;
        Task SaveAsync<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events) where TEvent : IDomainEvent;
    }
}
