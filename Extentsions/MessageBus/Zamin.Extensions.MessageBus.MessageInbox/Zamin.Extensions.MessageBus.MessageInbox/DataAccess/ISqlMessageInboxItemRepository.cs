namespace Zamin.Extensions.MessageBus.MessageInbox.DataAccess;

public interface ISqlMessageInboxItemRepository
{
    bool AllowReceive(string messageId, string fromService);
    void Receive(string messageId, string fromService);
}