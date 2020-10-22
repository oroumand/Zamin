using Zamin.Infra.Events.Outbox;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Zamin.Infra.Data.Sql.Commands.OutBoxEventItems
{
    public class SqlOutBoxEventItemRepository : IOutBoxEventItemRepository
    {
        string _connectionString;
        public SqlOutBoxEventItemRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ZaminConfigurations:Messaging:EventOutbox:ConnectionString"];
        }
        public List<OutBoxEventItem> GetOutBoxEventItemsForPublishe(int maxCount = 100)
        {
            using var connection = new SqlConnection(_connectionString);
            string query = $"Select top {maxCount} * from OutBoxEventItems where IsProcessed = 0";
            var result = connection.Query<OutBoxEventItem>(query).ToList();
            return result;

        }
        public void MarkAsRead(List<OutBoxEventItem> outBoxEventItems)
        {
            string idForMark = string.Join(',' ,outBoxEventItems.Where(c => c.IsProcessed).Select(c => c.OutBoxEventItemId).ToList());
            if (!string.IsNullOrWhiteSpace(idForMark))
            {
                using var connection = new SqlConnection(_connectionString);
                string query = $"Update OutBoxEventItems set IsProcessed = 1 where OutBoxEventItemId in ({idForMark})";
                connection.Execute(query);
            }
        }
    }
}
