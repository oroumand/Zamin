using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MiniBlog.Core.ApplicationService.Blogs.Events;
using MiniBlog.Endpoints.API.CustomDecorators;
using MiniBlog.Endpoints.API.Events;
using MiniBlog.Infra.Data.Sql.Commands.Common;
using MiniBlog.Infra.Data.Sql.Queries.Common;
using Serilog;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.ApplicationServices.Events;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.EndPoints.Web.Extentions.DependencyInjection;
using Zamin.Extensions.DependencyInjection;
using Zamin.Infra.Data.Sql.Commands.Interceptors;

namespace MiniBlog.Endpoints.API;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        string cnn = "Server =.; Database = MiniBlogDb; User Id = sa; Password = 1qaz!QAZ; MultipleActiveResultSets = true; Encrypt = false";
        builder.Services.AddZaminParrotTranslator(c =>
        {
            c.ConnectionString = cnn;
            c.AutoCreateSqlTable = false;
            c.SchemaName = "dbo";
            c.TableName = "ParrotTranslations";
            c.ReloadDataIntervalInMinuts = 1;
        });
        builder.Services.AddSingleton<CommandDispatcherDecorator, CustomCommandDecorator>();
        builder.Services.AddSingleton<QueryDispatcherDecorator, CustomQueryDecorator>();
        builder.Services.AddSingleton<EventDispatcherDecorator, CustomEventDecorator>();

        builder.Services.AddZaminWebUserInfoService(c => { c.DefaultUserId = "1"; }, true);

        builder.Services.AddZaminAutoMapperProfiles(option =>
        {
            option.AssmblyNamesForLoadProfiles = "MiniBlog";
        });

        builder.Services.AddZaminMicrosoftSerializer();

        builder.Services.AddZaminInMemoryCaching();

        builder.Services.AddDbContext<MiniblogCommandDbContext>(c => c.UseSqlServer(cnn).AddInterceptors(new SetPersianYeKeInterceptor(), new AddOutBoxEventItemInterceptor(), new AddAuditDataInterceptor()));
        builder.Services.AddDbContext<MiniblogQueryDbContext>(c => c.UseSqlServer(cnn));

        builder.Services.AddZaminApiCore("Zamin", "MiniBlog");
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddZaminRabbitMqMessageBus(c =>
        {
            c.PerssistMessage = true;
            c.ExchangeName = "SampleExchange";
            c.ApplicationName = "SampleApplciatoin";
            c.Url = @"amqp://guest:guest@localhost:5672/";
        });

        builder.Services.AddZaminPollingPublisher(c =>
        {
            c.ApplicationName = "SampleApplciatoin";
            c.ConnectionString = cnn;
            c.SelectCommand = "SELECT TOP (@Count) * FROM [MiniBlogDb].[dbo].[OutBoxEventItems] WHERE IsProcessed = 0";
            c.UpdateCommand = "UPDATE [MiniBlogDb].[dbo].[OutBoxEventItems] SET IsProcessed = 1 WHERE OutBoxEventItemId In @Ids";
        });
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
