using System.Data;
using System.Data.SqlClient;
using Zamin.Utilities.Configurations;
using Dapper;
using Zamin.Infra.Auth.ControllerDetectors.Models;
using System.Text;

namespace Zamin.Infra.Auth.ControllerDetectors.Data;
public class ControllerDetectorRepository
{
    private readonly ZaminConfigurationOptions _configuration;
    private readonly IDbConnection _dbConnection;
    private string _selectAllParts = "SELECT c.ApplicationId,app.ServiceName AS 'ApplicationName',a.ControllerId,c.[Name] AS 'ControllerName', a.Id AS ActionId, a.[Name] AS ActionName FROM {0}.{1} AS app LEFT JOIN {0}.{2} AS c ON app.Id = c.ApplicationId LEFT JOIN {0}.{3} AS a ON c.Id = a.ControllerId where app.ServiceName = @ServiceName";
    private string InsertApplicationCommand = "INSERT INTO {0}.{1}(Id,[ServiceName],[Title])values(@Id,@ServiceName,@ServiceName)";
    private string InsertControllerCommand = "If (Not Exists(Select * FROM {0}.{1} WHERE Name = @Name and ApplicationId=@ApplicationId)) BEGIN INSERT INTO {0}.{1}([ApplicationId],[Name],[Title])values(@ApplicationId,@Name,@Name) END Select Id FROM dbo.Applications WHERE ServiceName = @ServiceName";
    public ControllerDetectorRepository(ZaminConfigurationOptions configuration)
    {
        _configuration = configuration;
        _dbConnection = new SqlConnection(configuration.AppPart.ConnectionString);
        if (_configuration.AppPart.AutoCreateSqlTable)
            CreateTableIfNeeded();

        InsertApplicationCommand = string.Format(InsertApplicationCommand, _configuration.AppPart.SchemaName, _configuration.AppPart.ApplicationTableName);
        _selectAllParts = string.Format(_selectAllParts, _configuration.AppPart.SchemaName, _configuration.AppPart.ApplicationTableName, _configuration.AppPart.ControllerTableName, _configuration.AppPart.ActionTableName);
    }

    private void CreateTableIfNeeded()
    {
        string applicationTable = _configuration.AppPart.ApplicationTableName;
        string controllersTable = _configuration.AppPart.ControllerTableName;
        string actionTable = _configuration.AppPart.ActionTableName;
        string schema = _configuration.AppPart.SchemaName;

        string createApplicationTable = $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
            $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{applicationTable}' )) Begin " +
            $"CREATE TABLE [{schema}].[{applicationTable}]( " +
            $"[Id] [UNIQUEIDENTIFIER] NOT NULL Primary Key," +
            $"ServiceName NVARCHAR(100) NOT NULL UNIQUE," +
            $"Title NVARCHAR(100)," +
            $"[Description] NVARCHAR(500)," +
             $"Logo NVARCHAR(500))" +
            $" End";

        string createControllerTable = $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
            $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{controllersTable}' )) Begin " +
            $"CREATE TABLE [{schema}].[{controllersTable}]( " +
            $"[Id] [UNIQUEIDENTIFIER] NOT NULL Primary Key," +
            $"ApplicationId UNIQUEIDENTIFIER NOT NULL REFERENCES {schema}.{applicationTable}(Id)," +
            $"Name NVARCHAR(100) NOT NULL," +
            $"Title NVARCHAR(100)," +
            $"[Description] NVARCHAR(500)) " +
            $" End";

        string createActionTable = $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
            $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{actionTable}' )) Begin " +
            $"CREATE TABLE [{schema}].[{actionTable}]( " +
            $"[Id] [UNIQUEIDENTIFIER] NOT NULL Primary Key," +
            $"ControllerId UNIQUEIDENTIFIER NOT NULL REFERENCES {schema}.{controllersTable}(Id)," +
            $"Name NVARCHAR(100) NOT NULL," +
            $"Title NVARCHAR(100)," +
            $"[Description] NVARCHAR(500)) " +
            $" End";

        _dbConnection.Execute(createApplicationTable);
        _dbConnection.Execute(createControllerTable);
        _dbConnection.Execute(createActionTable);
    }

    public async Task<List<ApplicationControllerActionDto>> GetOldApplicationParts(string serviceName)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ServiceName", serviceName);
        return (await _dbConnection.QueryAsync<ApplicationControllerActionDto>(_selectAllParts, parameters)).ToList();
    }

    public async Task InsertNewActions(List<ActionData> actionForInsert)
    {
        if (actionForInsert != null && actionForInsert.Count > 0)
        {
            StringBuilder stringBuilder = new StringBuilder($"Insert into {_configuration.AppPart.SchemaName}.{_configuration.AppPart.ActionTableName}(Id,ControllerId,Name,Title) values ");
            for (int i = 0; i < actionForInsert.Count; i++)
            {
                stringBuilder.Append($"('{actionForInsert[i].Id}','{actionForInsert[i].ControllerId}','{actionForInsert[i].Name}','{actionForInsert[i].Name}')");

                stringBuilder.Append(i == (actionForInsert.Count - 1) ? ";" : ",");
            }
            await _dbConnection.ExecuteAsync(stringBuilder.ToString());
        }
    }

    public async Task InsertNewControllers(List<ControllerData> controllerDatas)
    {
        if (controllerDatas != null && controllerDatas.Count > 0)
        {
            StringBuilder stringBuilder = new($"Insert into {_configuration.AppPart.SchemaName}.{_configuration.AppPart.ControllerTableName}(Id,ApplicationId,Name,Title) values ");
            for (int i = 0; i < controllerDatas.Count; i++)
            {
                stringBuilder.Append($"('{controllerDatas[i].Id}','{controllerDatas[i].ApplicationId}','{controllerDatas[i].Name}','{controllerDatas[i].Name}')");
                stringBuilder.Append(i == (controllerDatas.Count - 1) ? ";" : ",");
            }
            await _dbConnection.ExecuteAsync(stringBuilder.ToString());

        }
    }

    public async Task<Guid> InsertApplication()
    {
        Guid applicationId = Guid.NewGuid();
        var parameters = new DynamicParameters();
        parameters.Add("@ServiceName", _configuration.ServiceName);
        parameters.Add("@Id", applicationId);
        await _dbConnection.ExecuteAsync(InsertApplicationCommand, param: parameters, commandType: CommandType.Text);
        return applicationId;
    }
}
