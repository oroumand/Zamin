using Microsoft.Extensions.DependencyInjection;
using Zamin.Extentions.MessageBus.Abstractions;

namespace Zamin.Extensions.MessageBus.MessageInbox.Extensions.DependencyInjection;

public static class MessageInboxServiceCollectionExtensions
{
    public static IServiceCollection AddZaminMessageInboxConsumer(this IServiceCollection services)
    {
        return services.AddScoped<IMessageConsumer, MessageConsumer>();
    }
}