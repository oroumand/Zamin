using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.Common;
using Zamin.Extensions.ChangeDataLog.Abstractions;
using Zamin.Extensions.ChangeDataLog.Sql.Options;

namespace Zamin.Extensions.ChangeDataLog.Sql;

public sealed class DapperEntityChangeInterceptorItemRepository : IEntityChageInterceptorItemRepository
{
    private readonly ChangeDataLogSqlOptions _options;
    private readonly string _insertEntityChangeInterceptorItemCommand;
    private readonly string _insertPropertyChangeLogItemCommand;
    private readonly ILogger<DapperEntityChangeInterceptorItemRepository> _logger;

    public DapperEntityChangeInterceptorItemRepository(IOptions<ChangeDataLogSqlOptions> options,
                                                       ILogger<DapperEntityChangeInterceptorItemRepository> logger)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));

        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Validate configuration
        if (string.IsNullOrWhiteSpace(_options.ConnectionString))
            throw new ArgumentException("Connection string cannot be null or empty.", nameof(_options.ConnectionString));

        if (string.IsNullOrWhiteSpace(_options.SchemaName))
            throw new ArgumentException("Schema name cannot be null or empty.", nameof(_options.SchemaName));

        if (string.IsNullOrWhiteSpace(_options.EntityTableName))
            throw new ArgumentException("Entity table name cannot be null or empty.", nameof(_options.EntityTableName));

        if (string.IsNullOrWhiteSpace(_options.PropertyTableName))
            throw new ArgumentException("Property table name cannot be null or empty.", nameof(_options.PropertyTableName));

        // Initialize SQL commands with proper formatting
        _insertEntityChangeInterceptorItemCommand = $@"
            INSERT INTO [{_options.SchemaName}].[{_options.EntityTableName}] 
                ([Id], [ContextName], [EntityType], [EntityId], [UserId], [IP], [TransactionId], [DateOfOccurrence], [ChangeType]) 
            VALUES 
                (@Id, @ContextName, @EntityType, @EntityId, @UserId, @IP, @TransactionId, @DateOfOccurrence, @ChangeType)";

        _insertPropertyChangeLogItemCommand = $@"
            INSERT INTO [{_options.SchemaName}].[{_options.PropertyTableName}] 
                ([Id], [ChangeInterceptorItemId], [PropertyName], [Value]) 
            VALUES 
                (@Id, @ChangeInterceptorItemId, @PropertyName, @Value)";

        if (_options.AutoCreateSqlTable)
        {
            using var connection = CreateDbConnection();
            connection.Open();
            CreateEntityChangeInterceptorItemTableIfNeeded(connection);
            CreatePropertyChangeLogItemTableIfNeeded(connection);
        }
    }

    public void Save(List<EntityChageInterceptorItem> entityChangeInterceptorItems, IDbTransaction transaction = null)
    {
        if (entityChangeInterceptorItems == null) throw new ArgumentNullException(nameof(entityChangeInterceptorItems));
        if (entityChangeInterceptorItems.Count == 0) return;

        bool externalTransactionProvided = transaction != null;
        DbConnection connection = externalTransactionProvided ? (DbConnection)transaction.Connection : CreateDbConnection();
        DbTransaction localTransaction = null;

        try
        {
            if (!externalTransactionProvided)
            {
                connection.Open();
                localTransaction = connection.BeginTransaction();
            }

            foreach (var item in entityChangeInterceptorItems)
            {
                connection.Execute(_insertEntityChangeInterceptorItemCommand, new
                {
                    item.Id,
                    item.ContextName,
                    item.EntityType,
                    item.EntityId,
                    item.UserId,
                    item.Ip,
                    item.TransactionId,
                    item.DateOfOccurrence,
                    item.ChangeType
                }, externalTransactionProvided ? transaction : localTransaction);

                if (item.PropertyChangeLogItems != null && item.PropertyChangeLogItems.Count > 0)
                {
                    foreach (var prop in item.PropertyChangeLogItems)
                    {
                        connection.Execute(_insertPropertyChangeLogItemCommand, new
                        {
                            prop.Id,
                            ChangeInterceptorItemId = item.Id,
                            prop.PropertyName,
                            prop.Value
                        }, externalTransactionProvided ? transaction : localTransaction);
                    }
                }
            }

            if (!externalTransactionProvided)
            {
                localTransaction.Commit();
            }
        }
        catch (Exception ex)
        {
            if (!externalTransactionProvided && localTransaction != null)
            {
                localTransaction.Rollback();
            }

            _logger.LogError(ex, "An error occurred while saving entity change interceptor items.");

            throw; // Rethrow the exception to let the caller handle it
        }
        finally
        {
            if (!externalTransactionProvided)
            {
                connection.Close();
                localTransaction?.Dispose();
            }
        }
    }

    public async Task SaveAsync(List<EntityChageInterceptorItem> entityChangeInterceptorItems, IDbTransaction transaction = null)
    {
        if (entityChangeInterceptorItems == null) throw new ArgumentNullException(nameof(entityChangeInterceptorItems));
        if (entityChangeInterceptorItems.Count == 0) return;

        bool externalTransactionProvided = transaction != null;
        DbConnection connection = externalTransactionProvided ? (DbConnection)transaction.Connection : CreateDbConnection();
        DbTransaction localTransaction = null;

        try
        {
            if (!externalTransactionProvided)
            {
                await connection.OpenAsync();
                localTransaction = connection.BeginTransaction();
            }

            foreach (var item in entityChangeInterceptorItems)
            {
                await connection.ExecuteAsync(_insertEntityChangeInterceptorItemCommand, new
                {
                    item.Id,
                    item.ContextName,
                    item.EntityType,
                    item.EntityId,
                    item.UserId,
                    item.Ip,
                    item.TransactionId,
                    item.DateOfOccurrence,
                    item.ChangeType
                }, externalTransactionProvided ? transaction : localTransaction);

                if (item.PropertyChangeLogItems != null && item.PropertyChangeLogItems.Count > 0)
                {
                    foreach (var prop in item.PropertyChangeLogItems)
                    {
                        await connection.ExecuteAsync(_insertPropertyChangeLogItemCommand, new
                        {
                            prop.Id,
                            ChangeInterceptorItemId = item.Id,
                            prop.PropertyName,
                            prop.Value
                        }, externalTransactionProvided ? transaction : localTransaction);
                    }
                }
            }

            if (!externalTransactionProvided)
            {
                localTransaction.Commit();
            }
        }
        catch (Exception ex)
        {
            if (!externalTransactionProvided && localTransaction != null)
            {
                localTransaction.Rollback();
            }

            _logger.LogError(ex, "An error occurred while saving entity change interceptor items.");

            throw; // Rethrow the exception to let the caller handle it
        }
        finally
        {
            if (!externalTransactionProvided)
            {
                connection.Close();
                localTransaction?.Dispose();
            }
        }
    }

    private DbConnection CreateDbConnection() => new SqlConnection(_options.ConnectionString);

    private void CreateEntityChangeInterceptorItemTableIfNeeded(DbConnection connection)
    {
        string createTable = $@"
            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = @Schema AND TABLE_NAME = @Table)
            BEGIN
                CREATE TABLE [{_options.SchemaName}].[{_options.EntityTableName}](
                    Id UNIQUEIDENTIFIER PRIMARY KEY, 
                    ContextName NVARCHAR(200) NOT NULL, 
                    EntityType NVARCHAR(200) NOT NULL, 
                    EntityId NVARCHAR(200) NOT NULL, 
                    UserId NVARCHAR(200), 
                    IP NVARCHAR(50),
                    TransactionId NVARCHAR(50),
                    DateOfOccurrence DATETIME NOT NULL,
                    ChangeType NVARCHAR(50)
                );
            END";
        connection.Execute(createTable, new { Schema = _options.SchemaName, Table = _options.EntityTableName });
    }

    private void CreatePropertyChangeLogItemTableIfNeeded(DbConnection connection)
    {
        string createTable = $@"
            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = @Schema AND TABLE_NAME = @Table)
            BEGIN
                CREATE TABLE [{_options.SchemaName}].[{_options.PropertyTableName}](
                    Id UNIQUEIDENTIFIER PRIMARY KEY, 
                    ChangeInterceptorItemId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [{_options.SchemaName}].[{_options.EntityTableName}](Id),
                    PropertyName NVARCHAR(200) NOT NULL,
                    Value NVARCHAR(MAX)
                );
            END";
        connection.Execute(createTable, new { Schema = _options.SchemaName, Table = _options.PropertyTableName });
    }
}