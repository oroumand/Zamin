namespace Zamin.Utilities.Configurations;
public class PoolingPublisherOptions
{
    public bool Enabled { get; set; } = true;
    public string OutBoxRepositoryTypeName { get; set; } = "SqlOutBoxEventItemRepository";
    public SqlOutBoxEventOptions SqlOutBoxEvent { get; set; } = new SqlOutBoxEventOptions();
    public int SendOutBoxInterval { get; set; } = 100;
    public int SendOutBoxCount { get; set; } = 5;
}
public class SqlOutBoxEventOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public string SelectCommand { get; set; } = "Select top {0} * from OutBoxEventItems where IsProcessed = 0";
    public string UpdateCommand { get; set; } = "Update OutBoxEventItems set IsProcessed = 1 where OutBoxEventItemId in ({0})";
}
