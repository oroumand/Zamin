using Dapper;
using System.Data.SqlClient;
using Zamin.Utilities.Configurations;

namespace Zamin.Messaging.IdempotentConsumers.Store.Sql;
public class SqlMessageInboxItemRepository : IMessageInboxItemRepository
{
    private readonly ZaminConfigurationOptions _configuration;
    readonly string _connectionString;

    public SqlMessageInboxItemRepository(ZaminConfigurationOptions configurations)
    {
        _connectionString = configurations.Messageconsumer.SqlMessageInboxStore.ConnectionString;
        _configuration = configurations;

        if (configurations.Messageconsumer.SqlMessageInboxStore.AutoCreateSqlTable)
            CreateTableIfNeeded();
    }

    public bool AllowReceive(string messageId, string fromService)
    {

        using var connection = new SqlConnection(_connectionString);

        string table = _configuration.Messageconsumer.SqlMessageInboxStore.TableName;
        string schema = _configuration.Messageconsumer.SqlMessageInboxStore.SchemaName;

        string query = $"Select Id from {schema}.[{table}] Where [OwnerService] = @OwnerService and [MessageId] = @MessageId";
        var result = connection.Query<long>(query, new
        {
            OwnerService = fromService,
            MessageId = messageId
        }).FirstOrDefault();
        return result < 1;
    }

    public void Receive(string messageId, string fromService)
    {
        using var connection = new SqlConnection(_connectionString);

        string table = _configuration.Messageconsumer.SqlMessageInboxStore.TableName;
        string schema = _configuration.Messageconsumer.SqlMessageInboxStore.SchemaName;

        string query = $"Insert Into {schema}.[{table}] ([OwnerService] ,[MessageId] ) values(@OwnerService,@MessageId)";
        var result = connection.Query<long>(query, new
        {
            OwnerService = fromService,
            MessageId = messageId
        }).FirstOrDefault();
    }

    private void CreateTableIfNeeded()
    {
        using var connection = new SqlConnection(_connectionString);

        string table = _configuration.Messageconsumer.SqlMessageInboxStore.TableName;
        string schema = _configuration.Messageconsumer.SqlMessageInboxStore.SchemaName;

        string createTable = $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
            $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{table}' )) " +
            $"Begin " +
            $"CREATE TABLE [dbo].[MessageInbox](" +
            $"[Id][bigint] IDENTITY(1, 1) Primary Key," +
            $"[OwnerService][nvarchar](100) NOT NULL," +
            $"   [MessageId][nvarchar](50) NOT NULL," +
            $"  [ReceivedDate] DateTime default(GetDate())," +
            $"    UNIQUE([OwnerService],[MessageId]))" +
            $" End";

        connection.Execute(createTable);
    }
}