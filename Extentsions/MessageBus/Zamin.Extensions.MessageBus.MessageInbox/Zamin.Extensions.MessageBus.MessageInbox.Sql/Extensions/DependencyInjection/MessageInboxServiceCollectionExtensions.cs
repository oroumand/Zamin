using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.MessageBus.MessageInbox.Abstractions;
using Zamin.Extensions.MessageBus.MessageInbox.Sql;
using Zamin.Extensions.MessageBus.MessageInbox.Sql.Options;

namespace Zamin.Extensions.MessageBus.MessageInbox.Extensions.DependencyInjection;

public static class MessageInboxServiceCollectionExtensions
{
    public static IServiceCollection AddZaminSqlMessageInboxRepository(this IServiceCollection services,
                                                                       IConfiguration configuration)
    {
        services.Configure<SqlMessageInboxOptions>(configuration);
        AddServices(services);
        return services;
    }

    public static IServiceCollection AddZaminSqlMessageInboxRepository(this IServiceCollection services,
                                                                       IConfiguration configuration,
                                                                       string sectionName)
    {
        services.AddZaminSqlMessageInboxRepository(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddZaminSqlMessageInboxRepository(this IServiceCollection services,
                                                                       Action<SqlMessageInboxOptions> setupAction)
    {
        services.Configure(setupAction);
        AddServices(services);
        return services;
    }

    private static IServiceCollection AddServices(IServiceCollection services)
    {
        return services.AddSingleton<IMessageInboxRepository, SqlMessageInboxRepository>();
    }
}