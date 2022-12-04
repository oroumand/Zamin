namespace Zamin.Extensions.MessageBus.MessageInbox.Abstractions;

public class FakeMessageInboxRepository : IMessageInboxRepository
{
    public bool AllowReceive(string messageId, string fromService) => true;

    public bool Receive(string messageId, string fromService) => true;
}