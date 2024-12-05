using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.Caching.Abstractions;
using Zamin.Extensions.Caching.Distributed.Sql.Options;
using Zamin.Extensions.Caching.Distributed.Sql.Services;

namespace Zamin.Extensions.DependencyInjection;

public static class DistributedSqlCacheServiceCollectionExtensions
{
    public static IServiceCollection AddZaminSqlDistributedCache(this IServiceCollection services, IConfiguration configuration, string sectionName)
        => services.AddZaminSqlDistributedCache(configuration.GetSection(sectionName));

    public static IServiceCollection AddZaminSqlDistributedCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DistributedSqlCacheOptions>(configuration);

        DistributedSqlCacheOptions options = configuration.Get<DistributedSqlCacheOptions>()
            ?? throw new ArgumentNullException(nameof(options));

        return services.AddServices(options);
    }

    public static IServiceCollection AddZaminSqlDistributedCache(this IServiceCollection services, Action<DistributedSqlCacheOptions> setupAction)
    {
        services.Configure(setupAction);
        DistributedSqlCacheOptions options = new();
        setupAction.Invoke(options);

        return services.AddServices(options);
    }

    private static IServiceCollection AddServices(this IServiceCollection services, DistributedSqlCacheOptions options)
    {
        services.AddTransient<ICacheAdapter, DistributedSqlCacheAdapter>();

        if (options.AutoCreateTable)
        {
            CreateTable(options);
        }

        services.AddDistributedSqlServerCache(configuration =>
        {
            configuration.ConnectionString = options.ConnectionString;
            configuration.SchemaName = options.SchemaName;
            configuration.TableName = options.TableName;
        });

        return services;
    }

    private static void CreateTable(DistributedSqlCacheOptions options)
    {
        string table = options.TableName;
        string schema = options.SchemaName;

        // SQL script to check and create schema if it doesn't exist
        string createSchema =
            $"IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = '{schema}')" + "\n" +
            $"BEGIN" + "\n" +
            $"EXEC('CREATE SCHEMA [{schema}]');" + "\n" +
            $"END";

        // SQL script to check and create table if it doesn't exist
        string createTable =
            $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{table}' ))" + "\n" +
            $"Begin" + "\n" +
            $"CREATE TABLE [{schema}].[{table}](" + "\n" +
            $"[Id][nvarchar](449) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL," + "\n" +
            $"[Value] [varbinary](max)NOT NULL," + "\n" +
            $"[ExpiresAtTime] [datetimeoffset](7) NOT NULL," + "\n" +
            $"[SlidingExpirationInSeconds] [bigint] NULL," + "\n" +
            $"[AbsoluteExpiration] [datetimeoffset](7) NULL," + "\n" +
            $"PRIMARY KEY(Id)," + "\n" +
            $"INDEX Index_ExpiresAtTime NONCLUSTERED (ExpiresAtTime))" + "\n" +
            $"End";

        using var dbConnection = new SqlConnection(options.ConnectionString);
        dbConnection.Open();

        // Execute the script to create schema if needed
        dbConnection.Execute(createSchema);

        // Execute the script to create table if needed
        dbConnection.Execute(createTable);
    }
}