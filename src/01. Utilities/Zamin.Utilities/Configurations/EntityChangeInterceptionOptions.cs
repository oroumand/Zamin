namespace Zamin.Utilities.Configurations
{
    public class EntityChangeInterceptionOptions
    {
        public bool Enabled { get; set; } = false;
        public string EntityChageInterceptorRepositoryTypeName { get; set; } = "FakeEntityChageInterceptorItemRepository";
        public DapperEntityChageInterceptorItemRepositoryOptions DapperEntityChageInterceptorItemRepository { get; set; }
            = new DapperEntityChageInterceptorItemRepositoryOptions();
    }


    public class DapperEntityChageInterceptorItemRepositoryOptions
    {

        public string ConnectionString { get; set; } = string.Empty;
        public bool AutoCreateSqlTable { get; set; } = true;
        public string EntityChageInterceptorItemTableName { get; set; } = "EntityChageInterceptorItem";
        public string EntityChageInterceptorItemSchemaName { get; set; } = "dbo";
        public string PropertyChangeLogItemTableName { get; set; } = "ParrotTranslations";
        public string PropertyChangeLogItemSchemaName { get; set; } = "PropertyChangeLogItem";
    }
}
