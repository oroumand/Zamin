using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Zamin.Extensions.MessageBus.RabbitMQ;
using Zamin.Extensions.MessageBus.RabbitMQ.Options;
using Zamin.Extentions.MessageBus.Abstractions;

namespace Zamin.Extensions.DependencyInjection;

public static class RabbitMqMessageBusServiceCollectionExtensions
{
    public static IServiceCollection AddZaminRabbitMqMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqOptions>(configuration);
        AddServices(services);
        return services;
    }

    public static IServiceCollection AddZaminRabbitMqMessageBus(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddZaminRabbitMqMessageBus(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddZaminRabbitMqMessageBus(this IServiceCollection services, Action<RabbitMqOptions> setupAction)
    {
        services.Configure(setupAction);
        AddServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<RabbitMqOptions>>();
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(options.Value.Url)
            };
            var connection = factory.CreateConnection();
            return connection;
        });
        services.AddScoped<ISendMessageBus, RabbitMqSendMessageBus>();
    }
}