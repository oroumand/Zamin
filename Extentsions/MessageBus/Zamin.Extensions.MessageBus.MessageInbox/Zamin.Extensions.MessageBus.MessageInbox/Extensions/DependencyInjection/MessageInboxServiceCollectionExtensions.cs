using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.MessageBus.Abstractions;
using Zamin.Extensions.MessageBus.MessageInbox;
using Zamin.Extensions.MessageBus.MessageInbox.Options;

namespace Zamin.Extensions.DependencyInjection;

public static class MessageInboxServiceCollectionExtensions
{
    public static IServiceCollection AddZaminMessageInbox(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MessageInboxOptions>(configuration);
        AddServices(services);
        return services;
    }

    public static IServiceCollection AddZaminMessageInbox(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddZaminMessageInbox(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddZaminMessageInbox(this IServiceCollection services, Action<MessageInboxOptions> setupAction)
    {
        services.Configure(setupAction);
        AddServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IMessageConsumer, InboxMessageConsumer>();
    }
}