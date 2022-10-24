using Zamin.Extensions.Events.PollingPublisher.Model;

namespace Zamin.Extensions.Events.PollingPublisher.DataAccess
{
    public interface IOutBoxEventItemRepository
    {
        public List<OutBoxEventItem> GetOutBoxEventItemsForPublishe(int maxCount = 100);
        void MarkAsRead(List<OutBoxEventItem> outBoxEventItems);
    }

}
