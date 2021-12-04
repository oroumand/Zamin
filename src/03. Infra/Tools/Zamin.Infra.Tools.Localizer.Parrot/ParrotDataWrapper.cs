﻿using System.Data;
using Dapper;
using System.Data.SqlClient;
using Zamin.Utilities.Configurations;

namespace Zamin.Infra.Tools.Localizer.Parrot;
public class ParrotDataWrapper
{
    private readonly IDbConnection _dbConnection;
    private readonly List<LocalizationRecord> _localizationRecords;
    private readonly ZaminConfigurationOptions _configuration;
    private string SelectCommand = "Select * from [{0}].[{1}]";
    private string InsertCommand = "INSERT INTO [{0}].[{1}]([Key],[Value],[Culture]) VALUES (@Key,@Value,@Culture) select SCOPE_IDENTITY()";


    public ParrotDataWrapper(ZaminConfigurationOptions configuration)
    {
        _configuration = configuration;
        _dbConnection = new SqlConnection(configuration.Translator.Parrottranslator.ConnectionString);
        if (_configuration.Translator.Parrottranslator.AutoCreateSqlTable)
            CreateTableIfNeeded();
        SelectCommand = string.Format(SelectCommand, configuration.Translator.Parrottranslator.SchemaName, configuration.Translator.Parrottranslator.TableName);

        InsertCommand = string.Format(InsertCommand, configuration.Translator.Parrottranslator.SchemaName, configuration.Translator.Parrottranslator.TableName);

        _localizationRecords = _dbConnection.Query<LocalizationRecord>(SelectCommand, commandType: CommandType.Text).ToList();
    }

    private void CreateTableIfNeeded()
    {
        string table = _configuration.Translator.Parrottranslator.TableName;
        string schema = _configuration.Translator.Parrottranslator.SchemaName;
        string createTable = $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
            $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{table}' )) Begin " +
            $"CREATE TABLE [{schema}].[{table}]( " +
            $"[Id] [int] IDENTITY(1,1) NOT NULL Primary Key," +
            $"[Key] [nvarchar](255) NOT NULL," +
            $"[Value] [nvarchar](500) NOT NULL," +
            $"[Culture] [nvarchar](5) NULL)" +
            $" End";
        _dbConnection.Execute(createTable);
    }

    public string Get(string key, string culture)
    {
        var record = _localizationRecords.FirstOrDefault(c => c.Key == key && c.Culture == culture);
        if (record == null)
        {
            record = new LocalizationRecord
            {
                Key = key,
                Culture = culture,
                Value = key
            };

            var parameters = new DynamicParameters();
            parameters.Add("@Key", key);
            parameters.Add("@Culture", culture);
            parameters.Add("@Value", key);

            record.Id = _dbConnection.Query<int>(InsertCommand, param: parameters, commandType: CommandType.Text).FirstOrDefault();
            _localizationRecords.Add(record);
        }
        return record.Value;
    }

}