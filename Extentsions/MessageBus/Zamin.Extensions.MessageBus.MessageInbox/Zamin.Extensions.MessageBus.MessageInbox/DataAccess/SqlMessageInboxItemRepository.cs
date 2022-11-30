using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Options;
using Zamin.Extensions.MessageBus.MessageInbox.Options;

namespace Zamin.Extensions.MessageBus.MessageInbox.DataAccess
{

    public class SqlMessageInboxItemRepository : IMessageInboxItemRepository
    {
        private readonly MessageInboxOptions _options;
        public SqlMessageInboxItemRepository(IOptions<MessageInboxOptions> options)
        {
            _options = options.Value;
        }

        public bool AllowReceive(string messageId, string fromService)
        {
            using var connection = new SqlConnection(_options.ConnectionString);
            string query = "Select Id from [MessageInbox] Where [OwnerService] = @OwnerService and [MessageId] = @MessageId";
            var result = connection.Query<long>(query, new
            {
                OwnerService = fromService,
                MessageId = messageId
            }).FirstOrDefault();
            return result < 1;
        }

        public void Receive(string messageId, string fromService)
        {
            using var connection = new SqlConnection(_options.ConnectionString);
            string query = "Insert Into [MessageInbox] ([OwnerService] ,[MessageId] ) values(@OwnerService,@MessageId)";
            var result = connection.Query<long>(query, new
            {
                OwnerService = fromService,
                MessageId = messageId
            }).FirstOrDefault();
        }
    }

}
