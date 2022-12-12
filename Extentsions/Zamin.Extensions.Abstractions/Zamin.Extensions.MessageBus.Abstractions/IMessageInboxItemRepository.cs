namespace Zamin.Extensions.MessageBus.Abstractions;
public interface IMessageInboxItemRepository
{
    bool AllowReceive(string messageId, string fromService);
    void Receive(string messageId, string fromService,string payload);
}
