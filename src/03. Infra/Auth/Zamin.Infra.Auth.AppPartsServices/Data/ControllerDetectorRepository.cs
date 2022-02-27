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
    private string _selectAllParts = "SELECT app.BusinessId as 'ServiceId',app.ServiceName,c.BusinessId as 'ControllerId',c.[Name] AS 'ControllerName', a.BusinessId AS ActionId, a.[Name] AS ActionName FROM {0}.{1} AS app LEFT JOIN {0}.{2} AS c ON app.BusinessId = c.ServiceId LEFT JOIN {0}.{3} AS a ON c.BusinessId = a.ControllerId where app.ServiceName = @ServiceName";
    private string InsertServiceCommand = "INSERT INTO {0}.{1}(BusinessId,[ServiceName],[Title],ModuleId)values(@BusinessId,@ServiceName,@ServiceName,@ModuleId)";
    private string InsertControllerCommand = "If (Not Exists(Select * FROM {0}.{1} WHERE Name = @Name and ApplicationId=@ApplicationId)) BEGIN INSERT INTO {0}.{1}([ApplicationId],[Name],[Title])values(@ApplicationId,@Name,@Name) END Select Id FROM dbo.Applications WHERE ServiceName = @ServiceName";
    public ControllerDetectorRepository(ZaminConfigurationOptions configuration)
    {
        _configuration = configuration;
        _dbConnection = new SqlConnection(configuration.AppPart.ConnectionString);
        if (_configuration.AppPart.AutoCreateSqlTable)
            CreateTableIfNeeded();

        InsertServiceCommand = string.Format(InsertServiceCommand, _configuration.AppPart.SchemaName, _configuration.AppPart.ServiceTableName);
        _selectAllParts = string.Format(_selectAllParts, _configuration.AppPart.SchemaName, _configuration.AppPart.ServiceTableName, _configuration.AppPart.ControllerTableName, _configuration.AppPart.ActionTableName);
    }

    private void CreateTableIfNeeded()
    {

        string applicationTable = _configuration.AppPart.ApplicationTableName;
        string moduleTable = _configuration.AppPart.ModuleTableName;


        string serviceTable = _configuration.AppPart.ServiceTableName;
        string controllersTable = _configuration.AppPart.ControllerTableName;
        string actionTable = _configuration.AppPart.ActionTableName;
        string schema = _configuration.AppPart.SchemaName;

        string createApplicatoinTable = $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
            $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{applicationTable}' )) Begin " +
            $"CREATE TABLE [{schema}].[{applicationTable}]( " +
            $"Id bigint  Primary Key Identity(1,1)," +
            $"[BusinessId] [UNIQUEIDENTIFIER] NOT NULL UNIQUE default(newId())," +
            $"ApplicationName NVARCHAR(100) NOT NULL UNIQUE," +
            $"Title NVARCHAR(100)," +
            $"[Description] NVARCHAR(500)," +
            $"Logo NVARCHAR(500)," +
            $"[URL] NVARCHAR(500))" +
            $" End";


        string createModeleTable = $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
            $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{moduleTable}' )) Begin " +
            $"CREATE TABLE [{schema}].[{moduleTable}]( " +
            $"Id bigint  Primary Key Identity(1,1)," +
            $"[BusinessId] [UNIQUEIDENTIFIER] NOT NULL UNIQUE default(newId())," +
            $"ApplicationId UNIQUEIDENTIFIER NOT NULL REFERENCES {schema}.{applicationTable}(BusinessId)," +
            $"ModuleName NVARCHAR(100) NOT NULL UNIQUE," +
            $"Title NVARCHAR(100)," +
            $"[Description] NVARCHAR(500)," +
            $"[SortOrder] int," +
            $"Logo NVARCHAR(500)," +
            $"[URL] NVARCHAR(500))" +
            $" End";



        string createServiceTable = $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
            $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{serviceTable}' )) Begin " +
            $"CREATE TABLE [{schema}].[{serviceTable}]( " +
            $"Id bigint  Primary Key Identity(1,1)," +
            $"[BusinessId] [UNIQUEIDENTIFIER] NOT NULL UNIQUE default(newId())," +
            $"ModuleId UNIQUEIDENTIFIER NOT NULL REFERENCES {schema}.{moduleTable}(BusinessId)," +
            $"ServiceName NVARCHAR(100) NOT NULL UNIQUE," +
            $"Title NVARCHAR(100)," +
            $"[Description] NVARCHAR(500)," +
            $"[SortOrder] int," +
            $"Logo NVARCHAR(500)," +
            $"[URL] NVARCHAR(500))" +
            $" End";

        string createControllerTable = $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
            $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{controllersTable}' )) Begin " +
            $"CREATE TABLE [{schema}].[{controllersTable}]( " +
            $"Id bigint  Primary Key Identity(1,1)," +
            $"[BusinessId] [UNIQUEIDENTIFIER] NOT NULL UNIQUE default(newId())," +
            $"ServiceId UNIQUEIDENTIFIER NOT NULL REFERENCES {schema}.{serviceTable}(BusinessId)," +
            $"Name NVARCHAR(100) NOT NULL," +
            $"Title NVARCHAR(100)," +
            $"URL NVARCHAR(500)," +
            $"LogoName NVARCHAR(500)," +
            $"[Description] NVARCHAR(500)) " +
            $" End";

        string createActionTable = $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
            $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{actionTable}' )) Begin " +
            $"CREATE TABLE [{schema}].[{actionTable}]( " +
            $"Id bigint Primary Key Identity(1,1)," +
            $"[BusinessId] [UNIQUEIDENTIFIER] NOT NULL UNIQUE default(newId())," +
            $"ControllerId UNIQUEIDENTIFIER NOT NULL REFERENCES {schema}.{controllersTable}(BusinessId)," +
            $"Name NVARCHAR(100) NOT NULL," +
            $"Title NVARCHAR(100)," +
            $"[Description] NVARCHAR(500)) " +
            $" End";

        _dbConnection.Execute(createApplicatoinTable);
        _dbConnection.Execute(createModeleTable);
        _dbConnection.Execute(createServiceTable);
        _dbConnection.Execute(createControllerTable);
        _dbConnection.Execute(createActionTable);
    }

    public async Task<ApplicationData> GetApplicationData(string applicationName)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@applicationName", applicationName);
        var result = await _dbConnection.QueryFirstOrDefaultAsync<ApplicationData>($"Select * from {_configuration.AppPart.SchemaName}.{_configuration.AppPart.ApplicationTableName} Where ApplicationName = @applicationName", parameters);
        return result;
    }

    internal async Task<Guid> InsertApplicationData(string applicationName)
    {
        Guid applicationId = Guid.NewGuid();
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationName", applicationName);
        parameters.Add("@BusinessId", applicationId);
        await _dbConnection.ExecuteAsync($"Insert Into {_configuration.AppPart.SchemaName}.{_configuration.AppPart.ApplicationTableName} (BusinessId,ApplicationName,Title) Values(@BusinessId,@ApplicationName,@ApplicationName)", parameters);
        return applicationId;
    }

    public async Task<ModuleData> GetModuleData(Guid applicationId, string moduleName)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);
        parameters.Add("@ModuleName", moduleName);
        var result = await _dbConnection.QueryFirstOrDefaultAsync<ModuleData>($"Select * from {_configuration.AppPart.SchemaName}.{_configuration.AppPart.ModuleTableName} Where ApplicationId = @ApplicationId and ModuleName = @ModuleName", parameters);
        return result;
    }
    internal async Task<Guid> InsertModuleData(Guid applicationId, string moduleName)
    {
        Guid moduleId = Guid.NewGuid();
        var parameters = new DynamicParameters();
        parameters.Add("@ModuleName", moduleName);
        parameters.Add("@ApplicationId", applicationId);
        parameters.Add("@BusinessId", moduleId);
        await _dbConnection.ExecuteAsync($"Insert Into {_configuration.AppPart.SchemaName}.{_configuration.AppPart.ModuleTableName} (BusinessId,ModuleName,ApplicationId,Title) Values(@BusinessId,@ModuleName,@ApplicationId,@ModuleName)", parameters);
        return moduleId;
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
            StringBuilder stringBuilder = new StringBuilder($"Insert into {_configuration.AppPart.SchemaName}.{_configuration.AppPart.ActionTableName}(BusinessId,ControllerId,Name,Title) values ");
            for (int i = 0; i < actionForInsert.Count; i++)
            {
                stringBuilder.Append($"('{actionForInsert[i].BusinessId}','{actionForInsert[i].ControllerId}','{actionForInsert[i].Name}','{actionForInsert[i].Name}')");

                stringBuilder.Append(i == (actionForInsert.Count - 1) ? ";" : ",");
            }
            await _dbConnection.ExecuteAsync(stringBuilder.ToString());
        }
    }

    public async Task InsertNewControllers(List<ControllerData> controllerDatas)
    {
        if (controllerDatas != null && controllerDatas.Count > 0)
        {
            StringBuilder stringBuilder = new($"Insert into {_configuration.AppPart.SchemaName}.{_configuration.AppPart.ControllerTableName}(BusinessId,ServiceId,Name,Title) values ");
            for (int i = 0; i < controllerDatas.Count; i++)
            {
                stringBuilder.Append($"('{controllerDatas[i].BusinessId}','{controllerDatas[i].ServiceId}','{controllerDatas[i].Name}','{controllerDatas[i].Name}')");
                stringBuilder.Append(i == (controllerDatas.Count - 1) ? ";" : ",");
            }
            var s = stringBuilder.ToString();
            await _dbConnection.ExecuteAsync(s);

        }
    }

    public async Task<Guid> InsertService(Guid modelId)
    {

        Guid applicationId = Guid.NewGuid();
        var parameters = new DynamicParameters();
        parameters.Add("@ServiceName", _configuration.ServiceName);
        parameters.Add("@BusinessId", applicationId);
        parameters.Add("@ModuleId", modelId);
        await _dbConnection.ExecuteAsync(InsertServiceCommand, param: parameters, commandType: CommandType.Text);
        return applicationId;
    }
}
