using Dapper;
using System.Data.SqlClient;
using Zamin.Utilities.Configurations;

namespace Zamin.Infra.Tools.Caching.Microsoft
{
    public static class DistributedSqlServerCacheExtentions
    {
        public static void CreateTable(ZaminConfigurationOptions options)
        {
            string table = options.Caching.DistributedSqlServerCache.TableName;
            string schema = options.Caching.DistributedSqlServerCache.SchemaName;

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

           var dbConnection = new SqlConnection(options.Caching.DistributedSqlServerCache.ConnectionString);
           dbConnection.Execute(createTable);
        }
    }
}