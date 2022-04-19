namespace Zamin.Messaging.IdempotentConsumers;
public interface IMessageInboxItemRepository
{
    bool AllowReceive(string messageId, string fromService);
    void Receive(string messageId, string fromService);
}
