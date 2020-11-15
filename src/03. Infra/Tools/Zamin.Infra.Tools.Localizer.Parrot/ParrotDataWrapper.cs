using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using System.Linq;
using Zamin.Utilities.Configurations;

namespace Zamin.Infra.Tools.Localizer.Parrot
{
    public class ParrotDataWrapper
    {
        private readonly IDbConnection _dbConnection;
        private readonly List<LocalizationRecord> _localizationRecords;
        private readonly ZaminConfigurations _configuration;

        public ParrotDataWrapper(ZaminConfigurations configuration)
        {
            _configuration = configuration;
            _dbConnection = new SqlConnection(configuration.Translator.Parrottranslator.ConnectionString);
            _localizationRecords = _dbConnection.Query<LocalizationRecord>(configuration.Translator.Parrottranslator.SelectCommand, commandType: CommandType.Text).ToList();
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
                string insertCommand = _configuration.Translator.Parrottranslator.InsertCommand;
                var parameters = new DynamicParameters();
                parameters.Add("@Key", key);
                parameters.Add("@Culture", culture);
                parameters.Add("@Value", key);

                record.Id = _dbConnection.Query<int>(insertCommand, param: parameters, commandType: CommandType.Text).FirstOrDefault();
                _localizationRecords.Add(record);
            }
            return record.Value;
        }

    }
}
