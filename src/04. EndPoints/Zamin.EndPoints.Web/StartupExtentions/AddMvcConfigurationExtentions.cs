using Zamin.EndPoints.Web.Filters;
using Zamin.EndPoints.Web.Middlewares.ApiExceptionHandler;
using Zamin.Utilities.Configurations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;

namespace Zamin.EndPoints.Web.StartupExtentions
{
    public static class AddMvcConfigurationExtentions
    {
        public static IServiceCollection AddZaminMvcServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var _hamoonConfigurations = new ZaminConfigurations();
            configuration.GetSection(nameof(ZaminConfigurations)).Bind(_hamoonConfigurations);
            services.AddSingleton(_hamoonConfigurations);
            services.AddScoped<ValidateModelStateAttribute>();
            services.AddControllersWithViews(options =>
            {
                //options.Filters.AddService<ValidateModelStateAttribute>();
                options.Filters.Add(typeof(TrackActionPerformanceFilter));
            }).AddRazorRuntimeCompilation()
            .AddFluentValidation();

            services.AddZaminDependencies(_hamoonConfigurations.AssmblyNameForLoad.Split(','));

            return services;
        }

        public static void UseZaminMvcConfigure(this IApplicationBuilder app, Action<IEndpointRouteBuilder> configur, ZaminConfigurations configuration, IWebHostEnvironment env)
        {
            app.UseApiExceptionHandler(options =>
            {
                options.AddResponseDetails = (context, ex, error) =>
                {
                    if (ex.GetType().Name == typeof(SqlException).Name)
                    {
                        error.Detail = "Exception was a database exception!";
                    }
                };
                options.DetermineLogLevel = ex =>
                {
                    if (ex.Message.StartsWith("cannot open database", StringComparison.InvariantCultureIgnoreCase) ||
                        ex.Message.StartsWith("a network-related", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return LogLevel.Critical;
                    }
                    return LogLevel.Error;
                };
            });
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(configur);
        }
    }
}
