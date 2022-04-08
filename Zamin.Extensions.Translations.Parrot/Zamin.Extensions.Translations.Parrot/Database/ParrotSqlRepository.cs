using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Logging;
using Zamin.Extensions.Translations.Parrot.DataModel;
using Zamin.Extensions.Translations.Parrot.Options;

namespace Zamin.Extensions.Translations.Parrot;
public class ParrotSqlRepository
{
    private readonly IDbConnection _dbConnection;
    private List<LocalizationRecord> _localizationRecords;
    private Timer _timer;

    private readonly ParrotTranslatorOptions _configuration;
    private readonly ILogger _logger;
    private readonly string _selectCommand = "Select * from [{0}].[{1}]";
    private readonly string _insertCommand = "INSERT INTO [{0}].[{1}]([Key],[Value],[Culture]) VALUES (@Key,@Value,@Culture) select SCOPE_IDENTITY()";


    public ParrotSqlRepository(ParrotTranslatorOptions configuration,ILogger logger)
    {
        _configuration = configuration;
        _logger = logger;
        _dbConnection = new SqlConnection(configuration.ConnectionString);

        if (_configuration.AutoCreateSqlTable)
            CreateTableIfNeeded();

        _selectCommand = string.Format(_selectCommand, configuration.SchemaName, configuration.TableName);

        _insertCommand = string.Format(_insertCommand, configuration.SchemaName, configuration.TableName);

        LoadLocalizationRecords(null);

        _timer = new Timer(LoadLocalizationRecords,
                           null,
                           TimeSpan.FromMinutes(configuration.ReloadDataIntervalInMinuts),
                           TimeSpan.FromMinutes(configuration.ReloadDataIntervalInMinuts));
    }

    private void LoadLocalizationRecords(object? state)
    {
        _logger.LogInformation("Parrot Translator load localization recored at {DateTime}", DateTime.Now);
       
        _localizationRecords = _dbConnection.Query<LocalizationRecord>(_selectCommand, commandType: CommandType.Text).ToList();
       
        _logger.LogInformation("Parrot Translator loaded localization recored at {DateTime}. Total record count is {RecordCount}", DateTime.Now, _localizationRecords.Count);
    }

    private void CreateTableIfNeeded()
    {
        try
        {
            string table = _configuration.TableName;
            string schema = _configuration.SchemaName;

            _logger.LogInformation("Parrot Translator try to create table in database with connection {ConnectionString}. Schema name is {Schema}. Table name is {TableName}", _configuration.ConnectionString,schema,table);


            string createTable = $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
                $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{table}' )) Begin " +
                $"CREATE TABLE [{schema}].[{table}]( " +
                $"Id bigint  Primary Key Identity(1,1)," +
                $"[BusinessId] [UNIQUEIDENTIFIER] NOT NULL UNIQUE  default(newId())," +
                $"[Key] [nvarchar](255) NOT NULL," +
                $"[Value] [nvarchar](500) NOT NULL," +
                $"[Culture] [nvarchar](5) NULL)" +
                $" End";
            _dbConnection.Execute(createTable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Create table for Parrot Translator Failed");
            throw;
        }
       
    }

    public string Get(string key, string culture)
    {
        try
        {
            var record = _localizationRecords.FirstOrDefault(c => c.Key == key && c.Culture == culture);
            if (record == null)
            {
                _logger.LogInformation("The key was not found and was registered with the default value in Parrot Translator. Key is {Key} and culture is {Culture}",key,culture);
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

                record.Id = _dbConnection.Query<int>(_insertCommand, param: parameters, commandType: CommandType.Text).FirstOrDefault();
                _localizationRecords.Add(record);
            }
            return record.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get Key value from Sql Server for Parrot Translator Failed");
            throw;
        }

    }

}