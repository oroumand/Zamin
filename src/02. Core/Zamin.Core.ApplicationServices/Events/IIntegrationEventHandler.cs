using System.Threading.Tasks;

namespace Zamin.Core.ApplicationServices.Events
{
    public interface IIntegrationEventHandler<TIntegrationEvent>
    where TIntegrationEvent : IIntegrationEvent
    {
        Task Handle(IIntegrationEvent Event);
    }
}
