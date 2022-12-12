using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using Zamin.Extensions.MessageBus.Abstractions;
using Zamin.Extensions.MessageBus.MessageInbox.Dal.Dapper.Options;

namespace Zamin.Extensions.MessageBus.MessageInbox.Dal.Dapper;
public class SqlMessageInboxItemRepository : IMessageInboxItemRepository
{
    private readonly IDbConnection _dbConnection;

    private readonly MessageInboxDalDapperOptions _options;
    private readonly string _selectQuery;
    private readonly string _insertQuery;
    private readonly ILogger<SqlMessageInboxItemRepository> _logger;

    public SqlMessageInboxItemRepository(IOptions<MessageInboxDalDapperOptions> options,ILogger<SqlMessageInboxItemRepository> logger)
    {
        _options = options.Value;
        _selectQuery = $"Select Id from {_options.SchemaName}.{_options.TableName} Where [OwnerService] = @OwnerService and [MessageId] = @MessageId";
        _insertQuery = $"Insert Into {_options.SchemaName}.{_options.TableName} ([OwnerService] ,[MessageId],[Payload] ) values(@OwnerService,@MessageId,@Payload)";
        _dbConnection = new SqlConnection(_options.ConnectionString);

        if (_options.AutoCreateSqlTable)
            CreateTableIfNeeded();
        _logger = logger;
    }

    public bool AllowReceive(string messageId, string fromService)
    {
        var result = _dbConnection.Query<long>(_selectQuery, new
        {
            OwnerService = fromService,
            MessageId = messageId
        }).FirstOrDefault();
        return result < 1;
    }

    public void Receive(string messageId, string fromService,string payload)
    {
        _dbConnection.Execute(_insertQuery, new
        {
            OwnerService = fromService,
            MessageId = messageId,
            Payload = payload
        });
    }


    private void CreateTableIfNeeded()
    {
        try
        {
            string table = _options.TableName;
            string schema = _options.SchemaName;

            _logger.LogInformation("Sql Message Inbox try to create table in database with connection {ConnectionString}. Schema name is {Schema}. Table name is {TableName}", _options.ConnectionString, schema, table);


            string createTable = $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
                $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{table}' )) Begin " +
                $"CREATE TABLE [{schema}].[{table}]( " +
                $"Id bigint  Primary Key Identity(1,1)," +
                $"[OwnerService] [nvarchar](100) NOT NULL," +
                $"[MessageId] [nvarchar](50) NOT NULL," +
                $"[Payload] [nvarchar](max) NULL," +
                $"[ReceivedDate] [DateTime] default(GetDate()))" +
                $" End";
            _dbConnection.Execute(createTable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Create table for Sql Message Inbox Failed");
            throw;
        }

    }
}