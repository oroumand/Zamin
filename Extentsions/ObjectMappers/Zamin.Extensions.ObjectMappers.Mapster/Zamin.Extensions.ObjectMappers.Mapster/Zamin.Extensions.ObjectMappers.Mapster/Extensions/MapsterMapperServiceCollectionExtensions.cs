using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.ObjectMappers.Mapster.Services;

namespace Zamin.Extensions.DependencyInjection;


public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMapsterMappings(this IServiceCollection services, params Assembly[] assembliesToScan)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        if (assembliesToScan == null || assembliesToScan.Length == 0)
            assembliesToScan = new[] { Assembly.GetCallingAssembly() };

        foreach (var assembly in assembliesToScan)
            config.Scan(assembly);

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        services.AddScoped<IObjectMapper, ObjectMappers.Mapster.Services.Mapster>();

        return services;
    }
}