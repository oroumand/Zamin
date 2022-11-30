using Zamin.Extensions.MessageBus.MessageInbox.Model;

namespace Zamin.Extensions.MessageBus.MessageInbox.DataAccess
{

    public interface IMessageInboxItemRepository
    {
        bool AllowReceive(string messageId, string fromService);
        void Receive(string messageId, string fromService);
    }

}
