using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MiniBlog.Endpoints.API.CustomDecorators;
using MiniBlog.Endpoints.API.Extentions.DependencyInjection.Swaggers.Extentions;
using MiniBlog.Infra.Data.Sql.Commands.Common;
using MiniBlog.Infra.Data.Sql.Queries.Common;
using Serilog;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.ApplicationServices.Events;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.EndPoints.Web.Extensions.ModelBinding;
using Zamin.Extensions.DependencyInjection;
using Zamin.Extensions.Events.Outbox.Dal.EF.Interceptors;
using Zamin.Infra.Data.Sql.Commands.Interceptors;

namespace MiniBlog.Endpoints.API.Extentions;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        IConfiguration configuration = builder.Configuration;

        builder.Services.AddSingleton<CommandDispatcherDecorator, CustomCommandDecorator>();
        builder.Services.AddSingleton<QueryDispatcherDecorator, CustomQueryDecorator>();
        builder.Services.AddSingleton<EventDispatcherDecorator, CustomEventDecorator>();

        //zamin
        builder.Services.AddZaminApiCore("Zamin", "ZaminTemplate");

        //microsoft
        builder.Services.AddEndpointsApiExplorer();

        //zamin
        builder.Services.AddZaminWebUserInfoService(configuration, "WebUserInfo", true);

        //zamin
        builder.Services.AddZaminParrotTranslator(configuration, "ParrotTranslator");

        //zamin
        //builder.Services.AddSoftwarePartDetector(configuration, "SoftwarePart");

        //zamin
        builder.Services.AddNonValidatingValidator();

        //zamin
        builder.Services.AddZaminMicrosoftSerializer();

        //zamin
        builder.Services.AddZaminAutoMapperProfiles(configuration, "AutoMapper");

        //zamin
        builder.Services.AddZaminInMemoryCaching();
        //builder.Services.AddZaminSqlDistributedCache(configuration, "SqlDistributedCache");

        //CommandDbContext
        builder.Services.AddDbContext<MiniblogCommandDbContext>(
            c => c.UseSqlServer(configuration.GetConnectionString("CommandDb_ConnectionString"))
            .AddInterceptors(new SetPersianYeKeInterceptor(),
                             new AddAuditDataInterceptor()));

        //QueryDbContext
        builder.Services.AddDbContext<MiniblogQueryDbContext>(
            c => c.UseSqlServer(configuration.GetConnectionString("QueryDb_ConnectionString")));

        //PollingPublisher
        //builder.Services.AddZaminPollingPublisherDalSql(configuration, "PollingPublisherSqlStore");
        //builder.Services.AddZaminPollingPublisher(configuration, "PollingPublisher");

        //MessageInbox
        //builder.Services.AddZaminMessageInboxDalSql(configuration, "MessageInboxSqlStore");
        //builder.Services.AddZaminMessageInbox(configuration, "MessageInbox");

        //builder.Services.AddZaminRabbitMqMessageBus(configuration, "RabbitMq");

        //builder.Services.AddZaminTraceJeager(configuration, "OpenTeletmetry");

        //builder.Services.AddIdentityServer(configuration, "OAuth");

        builder.Services.AddSwagger(configuration, "Swagger");

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        //zamin
        app.UseZaminApiExceptionHandler();

        //Serilog
        app.UseSerilogRequestLogging();

        app.UseSwaggerUI("Swagger");

        app.UseStatusCodePages();

        app.UseCors(delegate (CorsPolicyBuilder builder)
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });

        app.UseHttpsRedirection();

        //app.Services.ReceiveEventFromRabbitMqMessageBus(new KeyValuePair<string, string>("MiniBlog", "BlogCreated"));

        //var useIdentityServer = app.UseIdentityServer("OAuth");

        var controllerBuilder = app.MapControllers();

        //if (useIdentityServer)
        //    controllerBuilder.RequireAuthorization();

        //app.Services.GetService<SoftwarePartDetectorService>()?.Run();

        return app;
    }
}