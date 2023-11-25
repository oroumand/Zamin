using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics.Metrics;
using Zamin.Utilities.OpenTelemetryRegistration.Monitoring;
using Zamin.Utilities.OpenTelemetryRegistration.Options;

namespace Zamin.Extensions.DependencyInjection;
public static class OpenTeletmetryServiceCollectionExtensions
{
    public static IServiceCollection AddZaminObservabilitySupport(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<OpenTeletmetryOptions>(configuration);
        RegisterTraceServices(services);
        RegisterMetricService(services);
        return services;
    }

    public static IServiceCollection AddZaminObservabilitySupport(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddZaminObservabilitySupport(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddZaminObservabilitySupport(this IServiceCollection services, Action<OpenTeletmetryOptions> setupAction)
    {
        services.Configure(setupAction);
        RegisterTraceServices(services);
        RegisterMetricService(services);
        return services;
    }

    private static IServiceCollection RegisterTraceServices(IServiceCollection services)
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
                .SetSampler(new TraceIdRatioBasedSampler(options.SamplingProbability))
                .AddOtlpExporter(oltpOptions =>
                {
                    oltpOptions.Endpoint = new Uri(options.OltpEndpoint);
                    oltpOptions.ExportProcessorType = options.ExportProcessorType;
                });
            });
        return services;
    }



    public static IServiceCollection RegisterMetricService(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetRequiredService<IOptions<OpenTeletmetryOptions>>().Value;

        services.AddOpenTelemetry().WithMetrics(opts => opts
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(options.ApplicationName))
                    .AddMeter(options.ApplicationName)
                    .AddRuntimeInstrumentation()
                    .AddPrometheusExporter());

        services.AddSingleton(new MetricReporter(options.ApplicationName, options.ServiceName));
        return services;
    }


    public static IApplicationBuilder UseZaminObservabilityMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ResponseMetricMiddleware>();
        app.UseOpenTelemetryPrometheusScrapingEndpoint();
        return app;
    }
}
