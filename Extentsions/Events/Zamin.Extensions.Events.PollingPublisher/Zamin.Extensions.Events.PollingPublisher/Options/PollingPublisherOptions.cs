namespace Zamin.Extensions.Events.PollingPublisher.Options
{
    public class PollingPublisherOptions
    {
        public int SendInterval { get; set; } = 1000;
        public int ExceptionInterval { get; set; } = 10000;
        public int SendCount { get; set; } = 100;
        public string ConnectionString { get; set; }
        public string SelectCommand { get; set; } = "Select top (@Count) * from OutBoxEventItems where IsProcessed = 0";
        public string UpdateCommand { get; set; } = "Update OutBoxEventItems set IsProcessed = 1 where OutBoxEventItemId in @Ids";
        public string ApplicationName { get;  set; }
    }
}
