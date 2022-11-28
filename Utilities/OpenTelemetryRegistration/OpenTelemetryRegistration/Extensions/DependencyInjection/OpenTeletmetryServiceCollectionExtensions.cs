using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Zamin.Utilities.OpenTelemetryRegistration.Options;

namespace Zamin.Utilities.OpenTelemetryRegistration.Extensions.DependencyInjection;
public static class OpenTeletmetryServiceCollectionExtensions
{
    public static IServiceCollection AddZaminTraceJeager(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<OpenTeletmetryOptions>(configuration);
        AddJeagerTraceServices(services);
        return services;
    }

    public static IServiceCollection AddZaminTraceJeager(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddZaminTraceJeager(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddZaminTraceJeager(this IServiceCollection services, Action<OpenTeletmetryOptions> setupAction)
    {
        services.Configure(setupAction);
        AddJeagerTraceServices(services);
        return services;
    }

    private static IServiceCollection AddJeagerTraceServices(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetRequiredService<IOptions<OpenTeletmetryOptions>>().Value;
        services.AddOpenTelemetryTracing(tracerProviderBuilder =>
        {

            string serviceName = $"{options.ApplicationName}.{options.ServiceName}";
            tracerProviderBuilder
            .AddConsoleExporter()
            .AddJaegerExporter(c =>
            {
                c.AgentHost = options.AgentHost;
                c.AgentPort = options.AgentPort;
                c.ExportProcessorType = options.ExportProcessorType;
                c.MaxPayloadSizeInBytes = options.MaxPayloadSizeInBytes;
            })
            .AddSource(serviceName)
            .SetResourceBuilder(
                ResourceBuilder.CreateDefault()
                    .AddService(serviceName: serviceName, serviceVersion: options.ServiceVersion, serviceInstanceId: options.ServiceId))
            .AddHttpClientInstrumentation()
            .AddAspNetCoreInstrumentation()
            .AddSqlClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation();
        });


        services.AddOpenTelemetryMetrics(c =>
        {
            c.AddConsoleExporter();
            c.AddRuntimeInstrumentation();
            c.AddAspNetCoreInstrumentation();
        });

        return services;
    }
}
