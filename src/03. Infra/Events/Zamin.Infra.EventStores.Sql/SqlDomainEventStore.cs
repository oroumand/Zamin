using Zamin.Core.Domain.Data;
using Zamin.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Linq;
using Zamin.Utilities;

namespace Zamin.Infra.EventStores.Sql
{
    public class SqlDomainEventStore : IDomainEventStore
    {
        private readonly IDbConnection _dbConnection;
        private readonly ZaminApplicationContext _chichiContext;
        private ILogger _logger;
        public SqlDomainEventStore(IDbConnection dbConnection, ZaminApplicationContext chichiContext)
        {
            _dbConnection = dbConnection;
            _chichiContext = chichiContext;
            _logger = chichiContext.LoggerFactory.CreateLogger(nameof(SqlDomainEventStore));
            _logger.LogTrace("Create Instance Of SqlDomainEventStore");
        }
        public void Save<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events) where TEvent : IDomainEvent
        {
            var EventVersion = _dbConnection.Query<int>("SProc_DomainEvents_SelectVesion", new
            {
                StreamName = aggregateName,
                StreamId = aggregateId
            }, commandType: CommandType.StoredProcedure).FirstOrDefault() + 1;

            var occurDate = DateTime.Now;
            var userId = _chichiContext.UserInfoService.UserId();

            foreach (var @event in events)
            {
                _dbConnection.Execute("SProc_DomainEvents_Insert", new
                {
                    StreamName = aggregateName,
                    StreamId = aggregateId,
                    EventType = @event.GetType().FullName,
                    EventId = Guid.NewGuid().ToString(),
                    EventVersion,
                    EventPayload = _chichiContext.Serializer.Serilize(@event),
                    OccurDate = occurDate,
                    OccurByUserId = userId,
                    Handled = false
                }, commandType: CommandType.StoredProcedure);
            }

        }

        public Task SaveAsync<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events) where TEvent : IDomainEvent
        {
            throw new NotImplementedException();
        }
    }
}
