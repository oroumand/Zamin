using Dapper;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using Zamin.Extensions.ChangeDataLog.Sql.Options;
using Zamin.Extensions.ChangeDataLog.Abstractions;

namespace Zamin.Extensions.ChangeDataLog.Sql;
public class DapperEntityChageInterceptorItemRepository : IEntityChageInterceptorItemRepository
{
    private readonly ChangeDataLogSqlOptions _options;
    private readonly IDbConnection _dbConnection;
    private string InsertEntityChageInterceptorItemCommand = "INSERT INTO [{0}].[{1}]([Id],[ContextName],[EntityType],[EntityId],[UserId],[IP],[TransactionId],[DateOfOccurrence],[ChangeType]) VALUES (@Id,@ContextName,@EntityType,@EntityId,@UserId,@IP,@TransactionId,@DateOfOccurrence,@ChangeType) ";
    private string InsertPropertyChangeLogItemCommand = "INSERT INTO [{0}].[{1}]([Id],[ChageInterceptorItemId],[PropertyName],[Value]) VALUES (@Id,@ChageInterceptorItemId,@PropertyName,@Value)";

    public DapperEntityChageInterceptorItemRepository(IOptions<ChangeDataLogSqlOptions> options)
    {
        _options = options.Value;
        _dbConnection = new SqlConnection(_options.ConnectionString);
        if (_options.AutoCreateSqlTable)
        {
            CreateEntityChageInterceptorItemTableIfNeeded();
            CreatePropertyChangeLogItemTableIfNeeded();
        }

        InsertEntityChageInterceptorItemCommand = string.Format(InsertEntityChageInterceptorItemCommand, _options.SchemaName, _options.EntityTableName);
        InsertPropertyChangeLogItemCommand = string.Format(InsertPropertyChangeLogItemCommand, _options.SchemaName, _options.PropertyTableName);
    }
    public void Save(List<EntityChageInterceptorItem> entityChageInterceptorItems)
    {
        foreach (var item in entityChageInterceptorItems)
        {
            //if (_dbConnection.State == ConnectionState.Closed)
            //    _dbConnection.Open();
            //using var tran = _dbConnection.BeginTransaction();
            try
            {
                _dbConnection.Execute(InsertEntityChageInterceptorItemCommand, new { item.Id, item.ContextName, item.EntityType, item.EntityId, item.UserId, item.Ip, item.TransactionId, item.DateOfOccurrence, item.ChangeType });
                _dbConnection.Execute(InsertPropertyChangeLogItemCommand, item.PropertyChangeLogItems.ToArray());
                // tran.Commit();
            }
            catch (Exception ex)
            {

                // tran.Rollback();
            }
        }

    }

    public Task SaveAsync(List<EntityChageInterceptorItem> entityChageInterceptorItems)
    {
        throw new NotImplementedException();
    }



    private void CreateEntityChageInterceptorItemTableIfNeeded()
    {
        string table = _options.EntityTableName;
        string schema = _options.SchemaName;
        string createTable = $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
            $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{table}' )) Begin " +
            $"CREATE TABLE [{schema}].[{table}]( " +
                "Id uniqueidentifier primary key, " +
                "ContextName nvarchar(200)  not null, " +
                "EntityType nvarchar(200)  not null, " +
                "EntityId nvarchar(200)  not null, " +
                "UserId nvarchar(200) , " +
                "[IP] nvarchar(50)," +
                "TransactionId nvarchar(50) ," +
                "DateOfOccurrence Datetime  not null ," +
                "ChangeType nvarchar(50)" +
            $")" +
            $" End";
        _dbConnection.Execute(createTable);
    }


    private void CreatePropertyChangeLogItemTableIfNeeded()
    {
        string parentTable = _options.EntityTableName;
        string table = _options.PropertyTableName;
        string schema = _options.SchemaName;
        string createTable = $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
            $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{table}' )) Begin " +
            $"CREATE TABLE [{schema}].[{table}]( " +
                "Id uniqueidentifier primary key, " +
                $"ChageInterceptorItemId uniqueidentifier references [{schema}].[{parentTable}](Id)," +
                "PropertyName nvarchar(200)  not null," +
                "Value nvarchar(max) ," +
            $")" +
            $" End";
        _dbConnection.Execute(createTable);
    }
}
