namespace Zamin.Extensions.MessageBus.MessageInbox.Dal.Dapper.Options
{
    public class MessageInboxDalDapperOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
        //public string SelectCommand { get; set; } = "Select top (@Count) * from OutBoxEventItems where IsProcessed = 0";
        //public string UpdateCommand { get; set; } = "Update OutBoxEventItems set IsProcessed = 1 where OutBoxEventItemId in @Ids";
        public bool AutoCreateSqlTable { get; set; } = true;
        public string TableName { get; set; } = "MessageInbox";
        public string SchemaName { get; set; } = "zamin";

        //public string CreateTableText { get; set; } = string.Empty;
    }
}
