using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Linq;

namespace Zamin.EndPoints.Web
{
    public class ZaminProgram
    {
        public WebApplicationBuilder Main(string[] args, params string[] appSettingFiles)
        {
            AddBaseConfigs();

            try
            {
                StartLog();
                return CreateHostBuilder(args, appSettingFiles);
            }
            catch (Exception ex)
            {
                FatalLog(ex);
                return null;
            }
            finally
            {
                CloseAndFlushLog();
            }
        }

        public int Main(string[] args, Type startUp, params string[] appSettingFiles)
        {
            AddBaseConfigs();

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

            foreach (var item in appSettingFiles)
            {
                builder.Configuration.AddJsonFile(item, true);
            }

            return builder;
        }
        private IHostBuilder CreateHostBuilder(string[] args, Type startUp, params string[] appSettingFiles) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((ctx, config) =>
            {
                foreach (var item in appSettingFiles)
                {
                    config.AddJsonFile(item, true);
                }
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup(startUp);
                })
            .UseSerilog();

        private void AddBaseConfigs(params string[] appSettingFiles)
        {
            var configuration = CreateConfiguration();
            AddAppsettings(appSettingFiles);
            AddLogger(configuration);
        }

        private IConfiguration CreateConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();
            var configuration = configBuilder.Build();
            return configuration;
        }
        private void AddAppsettings(params string[] appSettingFiles)
        {
            if (appSettingFiles == null || !appSettingFiles.Any())
            {
                appSettingFiles = new string[] { "appsettings.json" };
            }
            var configBuilder = new ConfigurationBuilder();
            foreach (var item in appSettingFiles)
            {
                configBuilder.AddJsonFile(item);
            }
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
}
