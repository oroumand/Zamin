using Zamin.EndPoints.Web.Filters;
using Zamin.EndPoints.Web.Middlewares.ApiExceptionHandler;
using Zamin.Utilities.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;

namespace Zamin.EndPoints.Web.StartupExtentions
{
    public static class MvcConfigurationExtentions
    {
        public static IServiceCollection ZaminConfigureMvcServices<TResourceType>(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var _zaminConfigurations = new ZaminConfigurations();
            configuration.GetSection(nameof(ZaminConfigurations)).Bind(_zaminConfigurations);
            services.AddSingleton(_zaminConfigurations);

            
            services.AddControllersWithViews(options =>
            {
                options.Filters.AddService<ValidateModelStateAttribute>();
                options.Filters.Add(typeof(TrackActionPerformanceFilter));
            })

            .AddDataAnnotationsLocalization(options => options.DataAnnotationLocalizerProvider = (t, f) => f.Create(typeof(TResourceType)));
            services.AddZaminDependencies(_zaminConfigurations.AssmblyNameForLoad);

            return services;
        }
        public static void EveMvcConfigure(this IApplicationBuilder app, Action<IEndpointRouteBuilder> configur, ZaminConfigurations configuration)
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
