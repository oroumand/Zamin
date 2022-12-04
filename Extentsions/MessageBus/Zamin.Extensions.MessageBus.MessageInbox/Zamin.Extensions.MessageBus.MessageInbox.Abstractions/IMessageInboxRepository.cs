namespace Zamin.Extensions.MessageBus.MessageInbox.Abstractions;

public interface IMessageInboxRepository
{
    bool AllowReceive(string messageId, string fromService);

    bool Receive(string messageId, string fromService);
}