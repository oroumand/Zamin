using Dapper;
using Zamin.Infra.Data.ChangeInterceptors.EntityChageInterceptorItems;
using Zamin.Utilities.Configurations;
using System.Data;
using System.Data.SqlClient;

namespace Zamin.Infra.Data.ChangeInterceptors.Sql;
public class DapperEntityChageInterceptorItemRepository : IEntityChageInterceptorItemRepository
{
    private readonly ZaminConfigurationOptions _configuration;
    private readonly IDbConnection _dbConnection;
    private string InsertEntityChageInterceptorItemCommand = "INSERT INTO [{0}].[{1}]([Id],[ContextName],[EntityType],[EntityId],[UserId],[IP],[TransactionId],[DateOfOccurrence],[ChangeType]) VALUES (@Id,@ContextName,@EntityType,@EntityId,@UserId,@IP,@TransactionId,@DateOfOccurrence,@ChangeType) ";
    private string InsertPropertyChangeLogItemCommand = "INSERT INTO [{0}].[{1}]([Id],[ChageInterceptorItemId],[PropertyName],[Value]) VALUES (@Id,@ChageInterceptorItemId,@PropertyName,@Value)";

    public DapperEntityChageInterceptorItemRepository(ZaminConfigurationOptions configuration)
    {
        _configuration = configuration;
        _dbConnection = new SqlConnection(configuration.EntityChangeInterception.DapperEntityChageInterceptorItemRepository.ConnectionString);
        if (configuration.EntityChangeInterception.DapperEntityChageInterceptorItemRepository.AutoCreateSqlTable)
        {
            CreateEntityChageInterceptorItemTableIfNeeded();
            CreatePropertyChangeLogItemTableIfNeeded();
        }

        InsertEntityChageInterceptorItemCommand = string.Format(InsertEntityChageInterceptorItemCommand, configuration.EntityChangeInterception.DapperEntityChageInterceptorItemRepository.EntityChageInterceptorItemSchemaName, configuration.EntityChangeInterception.DapperEntityChageInterceptorItemRepository.EntityChageInterceptorItemTableName);
        InsertPropertyChangeLogItemCommand = string.Format(InsertPropertyChangeLogItemCommand, configuration.EntityChangeInterception.DapperEntityChageInterceptorItemRepository.PropertyChangeLogItemSchemaName, configuration.EntityChangeInterception.DapperEntityChageInterceptorItemRepository.PropertyChangeLogItemTableName);


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
        string table = _configuration.EntityChangeInterception.DapperEntityChageInterceptorItemRepository.EntityChageInterceptorItemTableName;
        string schema = _configuration.EntityChangeInterception.DapperEntityChageInterceptorItemRepository.EntityChageInterceptorItemSchemaName; ;
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
        string table = _configuration.EntityChangeInterception.DapperEntityChageInterceptorItemRepository.PropertyChangeLogItemTableName;
        string schema = _configuration.EntityChangeInterception.DapperEntityChageInterceptorItemRepository.PropertyChangeLogItemSchemaName; ;
        string createTable = $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
            $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{table}' )) Begin " +
            $"CREATE TABLE [{schema}].[{table}]( " +
                "Id uniqueidentifier primary key, " +
                "ChageInterceptorItemId uniqueidentifier references EntityChageInterceptorItem(Id)," +
                "PropertyName nvarchar(200)  not null," +
                "Value nvarchar(max) ," +
            $")" +
            $" End";
        _dbConnection.Execute(createTable);
    }
}
