using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using Zamin.Extensions.MessageBus.MessageInbox.Abstractions;
using Zamin.Extensions.MessageBus.MessageInbox.Abstractions.Options;

namespace Zamin.Extensions.MessageBus.MessageInbox.Sql;

public class SqlMessageInboxItemRepository : IMessageInboxItemRepository
{
    private readonly MessageInboxOptions _options;
    private readonly IDbConnection _dbConnection;
    private readonly ILogger<SqlMessageInboxItemRepository> _logger;

    private readonly string _selectCommand = "Select Id from [{0}].[{1}]  Where [OwnerService] = @OwnerService and [MessageId] = @MessageId";
    private readonly string _insertCommand = "INSERT INTO [{0}].[{1}]([Key],[Value],[Culture]) VALUES (@Key,@Value,@Culture) select SCOPE_IDENTITY()";

    public SqlMessageInboxItemRepository(IOptions<MessageInboxOptions> options, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<SqlMessageInboxItemRepository>();
        _options = options.Value;

        _dbConnection = new SqlConnection(_options.ConnectionString);

        if (_options.AutoCreateSqlTable)
            CreateTableIfNeeded();

        _selectCommand = string.Format(_selectCommand, _options.SchemaName, _options.TableName);
        _insertCommand = string.Format(_insertCommand, _options.SchemaName, _options.TableName);

        _logger.LogInformation("SqlMessageInboxItemRepository Start working");
    }

    private void CreateTableIfNeeded()
    {
        string table = _options.TableName;
        string schema = _options.SchemaName;
        try
        {
            _logger.LogInformation("SqlMessageInboxItemRepository try to create table in database with connection {ConnectionString}. Schema name is {Schema}. Table name is {TableName}", _options.ConnectionString, schema, table);

            string createTable = $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
                $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{table}' )) Begin " +
                $"CREATE TABLE [{schema}].[{table}]( " +
                $"[Id] [bigint] IDENTITY(1,1) Primary Key," +
                $"[OwnerService] [nvarchar](100) NOT NULL," +
                $"[MessageId] [nvarchar](50) NOT NULL," +
                $"[Data] [nvarchar](max) NOT NULL," +
                $"[ReceivedDate] [DateTime] default(GetDate())," +
                $"[IsProcessed] [bit] NOT NULL," +
                $"[ProcessedDate] DateTime NULL," +
                $"UNIQUE ([OwnerService],[MessageId]))" +
                $" End";

            _dbConnection.Execute(createTable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Create table for {Schema}.{TableName} Failed", schema, table);
            throw;
        }
    }

    public bool AllowReceive(string messageId, string fromService)
    {
        var result = _dbConnection.Query<long>(_selectCommand, new
        {
            OwnerService = fromService,
            MessageId = messageId
        }).FirstOrDefault();

        return result < 1;
    }

    public bool Receive(string messageId, string fromService)
    {
        var result = _dbConnection.Query<long>(_insertCommand, new
        {
            OwnerService = fromService,
            MessageId = messageId
        }).FirstOrDefault();

        return result >= 1;
    }
}
