using EventStore.ClientAPI;
using Zamin.Core.Domain.Data;
using Zamin.Core.Domain.Events;
using Zamin.Utilities.Services.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Zamin.Infra.EventStores.ES
{
    public class EsDomainEventStore : IDomainEventStore
    {
        private readonly ISerializer _serializer;

        public EsDomainEventStore(ISerializer serializer)
        {
            _serializer = serializer;
        }
        public void Save<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events) where TEvent : IDomainEvent
        {

         
        }




        public Task SaveAsync<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events) where TEvent : IDomainEvent
        {
            throw new NotImplementedException();
        }
    }
}
