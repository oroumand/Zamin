using Zamin.Extensions.DependencyInjection;
using Zamin.Utilities.SerilogRegistration.Extensions;
using Zamin.Utilities.Sinks.Elasticsearch.Sample;

SerilogExtensions.RunWithSerilogExceptionHandling(() =>
{
    var builder = WebApplication.CreateBuilder(args);

    var app = builder.AddZaminSerilog(c =>
    {
        c.ApplicationName = "SinksElasticsearch";
        c.ServiceName = "SampleService";
        c.ServiceVersion = "1.0";
        c.ServiceId = Guid.NewGuid().ToString();
    })
    .ConfigureServices()
    .ConfigurePipeline();

    app.Run();
});
