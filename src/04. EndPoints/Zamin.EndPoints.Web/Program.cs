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
