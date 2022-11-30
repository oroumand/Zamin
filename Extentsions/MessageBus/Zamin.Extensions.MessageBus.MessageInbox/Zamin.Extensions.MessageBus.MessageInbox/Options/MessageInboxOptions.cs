namespace Zamin.Extensions.MessageBus.MessageInbox.Options
{
    public class MessageInboxOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string SelectCommand { get; set; } = "Select top (@Count) * from OutBoxEventItems where IsProcessed = 0";
        public string UpdateCommand { get; set; } = "Update OutBoxEventItems set IsProcessed = 1 where OutBoxEventItemId in @Ids";
        public string ApplicationName { get; set; }
    }
}
