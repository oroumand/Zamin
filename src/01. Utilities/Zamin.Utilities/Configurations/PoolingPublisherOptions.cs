namespace Zamin.Utilities.Configurations
{
    public class PoolingPublisherOptions
    {
        public bool Enabled { get; set; }
        public string OutBoxRepositoryTypeName { get; set; }
        public SqlOutBoxEventOptions SqlOutBoxEvent { get; set; }
        public int SendOutBoxInterval { get; set; }
        public int SendOutBoxCount { get; set; }
    }
    public class SqlOutBoxEventOptions
    {
        public string ConnectionString { get; set; }
        public string SelectCommand { get; set; }
        public string UpdateCommand { get; set; }
    }
}
