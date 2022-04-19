namespace Zamin.Messaging.IdempotentConsumers;
public class MessageInboxItem
{
    public string MessageId { get; set; }
    public string OwnerService { get; set; }
    public DateTime ReceivedDate { get; set; }
}

