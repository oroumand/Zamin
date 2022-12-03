namespace Zamin.Extensions.MessageBus.MessageInbox.Abstractions;

public interface IMessageInboxItemRepository
{
    bool AllowReceive(string messageId, string fromService);

    bool Receive(string messageId, string fromService);
}