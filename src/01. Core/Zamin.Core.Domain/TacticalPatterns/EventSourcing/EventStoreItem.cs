using System;
using System.Collections.Generic;
using System.Text;

namespace Zamin.Core.Domain.TacticalPatterns.EventSourcing
{
    public class EventStoreItem
    {
        public Guid Id { get; set; }
        public string Data { get; set; }
        public int Version { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Aggregate { get; set; }
        public string AggregateId { get; set; }
    }
}
