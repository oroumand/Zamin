using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.MessageBus.MessageInbox.Abstractions;
using Zamin.Extensions.MessageBus.MessageInbox.Sql;

namespace Zamin.Extensions.MessageBus.MessageInbox.Extensions.DependencyInjection;

public static class MessageInboxServiceCollectionExtensions
{
    public static IServiceCollection AddZaminSqlMessageInboxItemRepository(this IServiceCollection services)
    {
        services.AddSingleton<IMessageInboxItemRepository, SqlMessageInboxItemRepository>();
        return services;
    }
}