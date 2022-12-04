using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.MessageBus.MessageInbox.Abstractions;

namespace Zamin.Extensions.MessageBus.MessageInbox.Extensions.DependencyInjection;

public static class MessageInboxServiceCollectionExtensions
{
    public static IServiceCollection AddZaminFakeMessageInboxRepository(this IServiceCollection services)
    {
        return services.AddSingleton<IMessageInboxRepository, FakeMessageInboxRepository>();
    }
}