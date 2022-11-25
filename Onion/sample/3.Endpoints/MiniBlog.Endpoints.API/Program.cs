using MiniBlog.Endpoints.API;
using Zamin.Extensions.DependencyInjection;
using Zamin.Utilities.SerilogRegistration.Extensions;

SerilogExtensions.RunWithSerilogExceptionHandling(() =>
{
    var builder = WebApplication.CreateBuilder(args);
    var app = builder.AddZaminSerilog(c =>
    {
        c.ApplicationName = "Miniblog";
        c.ServiceName = "MiniblogService";
        c.ServiceVersion = "1.0";
    }).ConfigureServices().ConfigurePipeline();
    app.Run();
});