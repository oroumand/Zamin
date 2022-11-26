using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Exceptions;
using Zamin.Utilities.SerilogRegistration.Enrichers;
using Zamin.Utilities.SerilogRegistration.Options;

namespace Zamin.Extensions.DependencyInjection;
public static class SerilogServiceCollectionExtensions
{
    public static WebApplicationBuilder AddZaminSerilog(this WebApplicationBuilder builder, IConfiguration configuration)
    {

        builder.Services.Configure<SerilogApplicationEnricherOptions>(configuration);
        return AddServices(builder);
    }

    public static WebApplicationBuilder AddZaminSerilog(this WebApplicationBuilder builder, IConfiguration configuration, string sectionName)
    {
        return builder.AddZaminSerilog(configuration.GetSection(sectionName));
    }

    public static WebApplicationBuilder AddZaminSerilog(this WebApplicationBuilder builder, Action<SerilogApplicationEnricherOptions> setupAction)
    {
        builder.Services.Configure(setupAction);
        return AddServices(builder);
    }

    private static WebApplicationBuilder AddServices(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ZaminUserInfoEnricher>();
        builder.Services.AddTransient<ZaminApplicaitonEnricher>();
        
        builder.Host.UseSerilog((ctx, services, lc) =>
        lc
        //.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .Enrich.With(services.GetRequiredService<ZaminUserInfoEnricher>())
        .Enrich.With(services.GetRequiredService<ZaminApplicaitonEnricher>())
        .Enrich.WithExceptionDetails()
        .Enrich.WithSpan()
        .ReadFrom.Configuration(ctx.Configuration));
        return builder;
    }
}
