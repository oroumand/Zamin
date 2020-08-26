using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamin.Core.Domain.TacticalPatterns;
using Zamin.Core.Domain.TacticalPatterns.EventSourcing;
using Zamin.Toolkits.Contracts;

namespace Zamin.Data.EventSourcing.SqlServer
{
    public class SqlEventStoreRepository : IEventStoreRepository
    {

        private readonly string _sqlConnectionString;
        private readonly ISerializerAdapter _serializer;

        public SqlEventStoreRepository(IConfiguration configuration, ISerializerAdapter serializer)
        {
            _sqlConnectionString = configuration.GetConnectionString("EventStoreDb");
            _serializer = serializer;
        }

        public async Task<IReadOnlyCollection<IDomainEvent>> LoadAsync(Guid aggregateRootId,
                                                                       string aggregateName)
        {
            if (aggregateRootId == null) throw new InvalidOperationException("AggregateRootId cannot be null");
            if (string.IsNullOrWhiteSpace(aggregateName)) throw new InvalidOperationException("AggregateName cannot be null");

            var query = $"SELECT * FROM EventStore WHERE [AggregateId] = @AggregateId and [Aggregate] = @Aggregate ORDER BY [Version] ASC";

            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                var events = (await sqlConnection.QueryAsync<EventStoreItem>(query.ToString(), new { AggregateId = aggregateRootId, Aggregate = aggregateName })).ToList();
                var domainEvents = events.Select(TransformEvent).Where(x => x != null).ToList().AsReadOnly();
                return domainEvents;
            }
        }
       
        private IDomainEvent TransformEvent(EventStoreItem eventSelected)
        {
            var o = _serializer.DeserializeObject(eventSelected.Data);
            var evt = o as IDomainEvent;
            return evt;
        }

        public async Task SaveAsync(Guid aggregateId, string aggregateName, int originatingVersion,
                                    IReadOnlyCollection<IDomainEvent> events)
        {
            if (events.Count < 1)
                return;

            var createdAt = DateTime.Now;

            string query = $"INSERT INTO EventStore(Id, CreatedAt, Version, Name, AggregateId, Data, Aggregate)" +
                $"VALUES (@Id,@CreatedAt,@Version,@Name,@AggregateId,@Data,@Aggregate)";

            var listOfEvents = events.Select(ev => new
            {
                Aggregate = aggregateName,
                CreatedAt = createdAt,
                Data = _serializer.SerilizeObject(ev),
                Id = Guid.NewGuid(),
                ev.GetType().Name,
                AggregateId = aggregateId.ToString(),
                Version = ++originatingVersion
            });

            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync(query, listOfEvents);
            }
        }

        public async Task<IReadOnlyCollection<EventStoreItem>> GetAll(DateTime? afterDateTime)
        {
            string where = afterDateTime.HasValue ? $"WHERE CreatedAt >  '{afterDateTime}' " : "";
            var query = $"SELECT * FROM EventStore {where} ORDER BY CreatedAt,[Version] ASC";

            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                var events = (await sqlConnection.QueryAsync<EventStoreItem>(query.ToString())).ToList();

                return events;
            }
        }
    }
}
