using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using System.Linq;

namespace Zamin.Infra.Tools.Localizer.Parrot
{
    public class ParrotDataWrapper
    {
        private readonly IDbConnection _dbConnection;
        private readonly List<LocalizationRecord> _localizationRecords;
        public ParrotDataWrapper(IConfiguration configuration)
        {
            var cnnString = configuration["ZaminConfigurations:Translator:ParrotTranslator:ConnectionString"];
            _dbConnection = new SqlConnection(cnnString);
            string selectCommand = "Select * from ParrotTranslations";
            _localizationRecords = _dbConnection.Query<LocalizationRecord>(selectCommand, commandType: CommandType.Text).ToList();
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
                string insertCommand = "INSERT INTO [dbo].[ParrotTranslations]([Key],[Value],[Culture]) VALUES (@Key,@Value,@Culture) select SCOPE_IDENTITY()";
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
