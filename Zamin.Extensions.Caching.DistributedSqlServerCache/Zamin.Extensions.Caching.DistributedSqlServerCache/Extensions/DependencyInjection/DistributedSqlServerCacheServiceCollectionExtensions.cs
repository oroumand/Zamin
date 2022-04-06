using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.Caching.DistributedSqlServerCache.Options;
using Zamin.Extensions.Caching.DistributedSqlServerCache.Services;
using Zamin.Extentions.Chaching.Abstractions;

namespace Zamin.Extensions.Caching.DistributedSqlServerCache.Extensions.DependencyInjection;

public static class DistributedSqlServerCacheServiceCollectionExtensions
{
    public static IServiceCollection AddSqlServerDistributedCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ICacheAdapter, DistributedCacheAdapter>();
        services.Configure<DistributedSqlServerCacheOptions>(configuration);

        var config = new DistributedSqlServerCacheOptions();
        configuration.Bind(config);
        if(config.AutoCreateSqlTable)
            CreateTable(config);

        services.AddDistributedSqlServerCache(options =>
        {
            options.ConnectionString = config.ConnectionString;
            options.SchemaName = config.SchemaName;
            options.TableName = config.TableName;
        });

        return services;
    }

    public static IServiceCollection AddSqlServerDistributedCache(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddSqlServerDistributedCache(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddSqlServerDistributedCache(this IServiceCollection services, Action<DistributedSqlServerCacheOptions> setupAction)
    {
        services.AddTransient<ICacheAdapter, DistributedCacheAdapter>();
        services.Configure(setupAction);

        var config = new DistributedSqlServerCacheOptions();
        setupAction.Invoke(config);
        if (config.AutoCreateSqlTable)
            CreateTable(config);
        services.AddDistributedSqlServerCache(options =>
        {
            options.ConnectionString = config.ConnectionString;
            options.SchemaName = config.SchemaName;
            options.TableName = config.TableName;
        });

        return services;
    }


    private static void CreateTable(DistributedSqlServerCacheOptions options)
    {
        string table = options.TableName;
        string schema = options.SchemaName;

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

        var dbConnection = new SqlConnection(options.ConnectionString);
        dbConnection.Execute(createTable);
    }
}
