using Serilog;
using Zamin.Extensions.DependencyInjection;

namespace MiniBlog.Endpoints.API;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddZaminApiCore("Zamin", "MiniBlog");
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder.Build();
    }
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseZaminApiExceptionHandler();
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();


        return app;
    }
}
