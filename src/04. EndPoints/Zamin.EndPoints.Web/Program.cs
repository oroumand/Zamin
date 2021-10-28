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

            if (appSettingFiles == null || !appSettingFiles.Any())
            {
                appSettingFiles = new string[] { "appsettings.json" };
            }
            var configBuilder = new ConfigurationBuilder();
            foreach (var item in appSettingFiles)
            {
                configBuilder.AddJsonFile(item);
            }
            var configuration = configBuilder.Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            try
            {
                Log.Information("Starting web host");
                return CreateHostBuilder(args, appSettingFiles);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return null;
            }
            finally
            {
                Log.CloseAndFlush();
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

        public int Main(string[] args, Type startUp, params string[] appSettingFiles)
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
            var configuration = configBuilder.Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args, startUp, appSettingFiles).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
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
    }
}
