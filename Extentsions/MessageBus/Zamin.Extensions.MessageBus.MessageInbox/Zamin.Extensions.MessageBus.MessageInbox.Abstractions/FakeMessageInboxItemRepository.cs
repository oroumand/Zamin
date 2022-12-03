namespace Zamin.Extensions.MessageBus.MessageInbox.Abstractions;

public class FakeMessageInboxItemRepository : IMessageInboxItemRepository
{
    public bool AllowReceive(string messageId, string fromService) => true;

    public bool Receive(string messageId, string fromService) => true;
}