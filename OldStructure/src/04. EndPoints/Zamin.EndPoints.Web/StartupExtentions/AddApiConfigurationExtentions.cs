using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using System.Data.SqlClient;
using Zamin.EndPoints.Web.Filters;
using Zamin.EndPoints.Web.Middlewares.ApiExceptionHandler;

namespace Zamin.EndPoints.Web.StartupExtentions
{
    public static class AddApiConfigurationExtentions
    {
        public static IServiceCollection AddZaminApiServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var _zaminConfigurations = new ZaminConfigurationOptions();
            configuration.GetSection(_zaminConfigurations.SectionName).Bind(_zaminConfigurations);
            services.AddSingleton(_zaminConfigurations);
            services.AddScoped<ValidateModelStateAttribute>();
            services.AddControllers(options =>
            {
                options.Filters.AddService<ValidateModelStateAttribute>();
                options.Filters.Add(typeof(TrackActionPerformanceFilter));
            }).AddFluentValidation();

            services.AddEndpointsApiExplorer();

            services.AddZaminDependencies(_zaminConfigurations.AssmblyNameForLoad.Split(','));

            AddSwagger(services);
            return services;
        }

        private static void AddSwagger(IServiceCollection services)
        {
            var _zaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurationOptions>();
            if (_zaminConfigurations?.Swagger?.Enabled == true && _zaminConfigurations.Swagger.SwaggerDoc != null)
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(_zaminConfigurations.Swagger.SwaggerDoc.Name, new OpenApiInfo { Title = _zaminConfigurations.Swagger.SwaggerDoc.Title, Version = _zaminConfigurations.Swagger.SwaggerDoc.Version });
                });
            }
        }
        public static void UseZaminApiConfigure(this IApplicationBuilder app, ZaminConfigurationOptions configuration, IWebHostEnvironment env)
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
            if (configuration.Swagger != null && configuration.Swagger.SwaggerDoc != null)
            {

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(configuration.Swagger.SwaggerDoc.URL, configuration.Swagger.SwaggerDoc.Title);
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
            if(configuration.UseHttpsRedirection)
                app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }




    }
}
