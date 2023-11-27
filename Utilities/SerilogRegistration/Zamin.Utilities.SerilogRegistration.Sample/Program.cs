using Zamin.Extensions.DependencyInjection;
using Zamin.Utilities.SerilogRegistration.Extensions;
using Zamin.Utilities.SerilogRegistration.Sample;
using Zamin.Utilities.SerilogRegistration.Sample.SampleEnrichers;
SerilogExtensions.RunWithSerilogExceptionHandling(() =>
{
    var builder = WebApplication.CreateBuilder(args);
    var app = builder.AddZaminSerilog(c =>
    {
        c.ApplicationName = "SerilogRegistration";
        c.ServiceName = "SampleService";
        c.ServiceVersion = "1.0";
        c.ServiceId= Guid.NewGuid().ToString();
    },typeof(Sample01Enricher),typeof(Sample02Enricher)).ConfigureServices().ConfigurePipeline();
    app.Run();
});
