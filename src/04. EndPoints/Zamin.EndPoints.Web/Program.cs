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
        AddBaseConfigs(appSettingFiles);

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

        AddBaseConfigs(builder.Configuration, appSettingFiles);

        return builder;
    }
    private IHostBuilder CreateHostBuilder(string[] args, Type startUp, params string[] appSettingFiles) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((ctx, config) =>
        {
            AddBaseConfigs(config, appSettingFiles);
        })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup(startUp);
            })
        .UseSerilog();

    private void AddBaseConfigs(params string[] appSettingFiles)
    {
        var configuration = CreateConfiguration(appSettingFiles);
        AddLogger(configuration);
    }


    private void AddBaseConfigs(IConfigurationBuilder configurationBuilder, params string[] appSettingFiles)
    {
        var configuration = AddAppsettings(configurationBuilder, appSettingFiles);
        AddLogger(configuration.Build());
    }

    private void AddBaseConfigs(ConfigurationManager configurationManager, params string[] appSettingFiles)
    {
        AddAppsettings(configurationManager, appSettingFiles);

        AddLogger(configurationManager);
    }

    private IConfiguration CreateConfiguration(params string[] appSettingFiles)
    {
        var configBuilder = new ConfigurationManager();
        var configurationBuilder = AddAppsettings(configBuilder, appSettingFiles);
        return configurationBuilder;
    }

    private ConfigurationManager AddAppsettings(ConfigurationManager configurationManager, params string[] appSettingFiles)
    {
        if (appSettingFiles == null || !appSettingFiles.Any())
        {
            appSettingFiles = new string[] { "appsettings.json" };
        }
        foreach (var item in appSettingFiles)
        {
            configurationManager.AddJsonFile(item);
        }

        return configurationManager;
    }

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