namespace Zamin.EndPoints.Web;

public class ZaminProgram
{
    public WebApplicationBuilder Main(string[] args, params string[] appSettingFiles)
    {
        try
        {
            StartLog();
            return CreateHostBuilder(args, appSettingFiles);
        }
        catch (Exception ex)
        {
            FatalLog(ex);
            throw;
        }
        finally
        {
            CloseAndFlushLog();
        }
    }

    public int Main(string[] args, Type startUp, params string[] appSettingFiles)
    {
        try
        {
            StartLog();
            CreateHostBuilder(args, startUp, appSettingFiles).Build().Run();
            return 0;
        }
        catch (Exception ex)
        {
            FatalLog(ex);
            return 1;
        }
        finally
        {
            CloseAndFlushLog();
        }
    }

    private WebApplicationBuilder CreateHostBuilder(string[] args, params string[] appSettingFiles)
    {
        var builder = WebApplication.CreateBuilder(args);

        AddAppsettings(builder.Configuration, appSettingFiles);

        AddLogger(builder);

        return builder;
    }
    private IHostBuilder CreateHostBuilder(string[] args, Type startUp, params string[] appSettingFiles) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((ctx, config) =>
        {
            AddAppsettings(config, appSettingFiles);
            AddLogger(config.Build());
        })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup(startUp);
            })
        .UseSerilog();

    private IConfigurationBuilder AddAppsettings(IConfigurationBuilder configurationBuilder, params string[] appSettingFiles)
    {
        if (appSettingFiles == null || !appSettingFiles.Any())
        {
            appSettingFiles = new string[] { "appsettings.json" };
        }
        foreach (var item in appSettingFiles)
        {
            configurationBuilder.AddJsonFile(item);
        }

        return configurationBuilder;
    }

    private void AddLogger(IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }

    private void AddLogger(WebApplicationBuilder builder)
    {
        AddLogger(builder.Configuration);
        builder.Host.UseSerilog(Log.Logger);
    }

    private void StartLog()
    {
        Log.Information("Starting web host");
    }
    private void FatalLog(Exception ex)
    {
        Log.Fatal(ex, "Host terminated unexpectedly");
    }
    private void CloseAndFlushLog()
    {
        Log.CloseAndFlush();
    }
}