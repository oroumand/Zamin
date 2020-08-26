using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zamin.Core.Domain.TacticalPatterns
{
    public abstract class AggregateRoot : Entity
    {
        private readonly List<IDomainEvent> _domainEvents;
        protected AggregateRoot() => _domainEvents = new List<IDomainEvent>();
        public IEnumerable<IDomainEvent> GetEvents() => _domainEvents.AsEnumerable();      
        protected void AddDomainEvent(IDomainEvent @event)
        {
            _domainEvents.Add(@event);
        }
        protected void RemoveDomainEvent(IDomainEvent @event) =>
           _domainEvents.Remove(@event);
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
