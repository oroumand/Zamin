using Microsoft.EntityFrameworkCore;
using MiniBlog.Endpoints.API.CustomDecorators;
using MiniBlog.Infra.Data.Sql.Commands.Common;
using MiniBlog.Infra.Data.Sql.Queries.Common;
using Serilog;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.ApplicationServices.Events;
using Zamin.Core.ApplicationServices.Queries;
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
            c.AutoCreateSqlTable = true;
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

        builder.Services.AddDbContext<MiniblogCommandDbContext>(c => c.UseSqlServer(cnn).AddInterceptors(new SetPersianYeKeInterceptor(), new AddAuditDataInterceptor()));
        builder.Services.AddDbContext<MiniblogQueryDbContext>(c => c.UseSqlServer(cnn));

        builder.Services.AddZaminApiCore("Zamin", "MiniBlog");
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();      

        builder.Services.AddZaminTraceJeager(c =>
        {
            c.AgentHost = "localhost";
            c.ApplicationName = "MiniBlog";
            c.ServiceName = "OpenTelemetrySample";
            c.ServiceVersion = "1.0.0";
            c.ServiceId = "cb387bb6-9a66-444f-92b2-ff64e2a81f98";
        });

        builder.Services.AddZaminRabbitMqMessageBus(c =>
        {
            c.PerssistMessage = false;
            c.ExchangeName = "MiniBlogExchange";
            c.ServiceName = "MiniBlogAPI";
            c.Url = @"amqp://guest:guest@localhost:5672/";
        });

        builder.Services.AddZaminPollingPublisherDalSql(c =>
        {
            c.ApplicationName = "MiniBlog";
            c.ConnectionString = cnn;           
        });
     
        builder.Services.AddZaminPollingPublisher(c =>
        {
            c.SendInterval = 1000;
            c.SendCount = 100;
            c.ExceptionInterval = 10000;
            c.ApplicationName = "MiniBlog";
        });


        builder.Services.AddZaminMessageInbox(c =>
        {
            c.ApplicationName = "MiniBlog";
        });
        builder.Services.AddZaminMessageInboxDalSql(c =>
        {
            //c.TableName = "MessageInbox";
            c.SchemaName = "dbo";
            c.ConnectionString = cnn;
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
       
        app.Services.ReceiveEventFromRabbitMqMessageBus(new KeyValuePair<string, string>("MiniBlog", "BlogCreated"));

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
