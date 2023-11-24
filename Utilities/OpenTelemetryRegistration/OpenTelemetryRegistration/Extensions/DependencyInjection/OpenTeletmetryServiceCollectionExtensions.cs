using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Zamin.Utilities.OpenTelemetryRegistration.Options;

namespace Zamin.Extensions.DependencyInjection;
public static class OpenTeletmetryServiceCollectionExtensions
{
    public static IServiceCollection AddZaminTraceSupport(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<OpenTeletmetryOptions>(configuration);
        AddTraceServices(services);
        return services;
    }

    public static IServiceCollection AddZaminTraceSupport(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddZaminTraceSupport(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddZaminTraceSupport(this IServiceCollection services, Action<OpenTeletmetryOptions> setupAction)
    {
        services.Configure(setupAction);
        AddTraceServices(services);
        return services;
    }

    private static IServiceCollection AddTraceServices(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetRequiredService<IOptions<OpenTeletmetryOptions>>().Value;

        services.AddOpenTelemetry()
            .WithTracing(tracerProviderBuilder =>
            {
                string serviceName = $"{options.ApplicationName}.{options.ServiceName}";
                tracerProviderBuilder
                .SetResourceBuilder(ResourceBuilder.CreateDefault()
                        .AddService(serviceName: serviceName, serviceVersion: options.ServiceVersion, serviceInstanceId: options.ServiceId))
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddSqlClientInstrumentation()
                .AddEntityFrameworkCoreInstrumentation()
                .AddOtlpExporter(oltpOptions =>
                {
                    oltpOptions.Endpoint = new Uri(options.OltpEndpoint);
                    oltpOptions.ExportProcessorType = options.ExportProcessorType;
                });
            });


        return services;
    }
}
