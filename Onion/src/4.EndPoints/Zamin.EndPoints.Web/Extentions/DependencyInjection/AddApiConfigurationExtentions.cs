using FluentValidation.AspNetCore;
using System.Data.SqlClient;
using Zamin.EndPoints.Web.Filters;
using Zamin.EndPoints.Web.Middlewares.ApiExceptionHandler;

namespace Zamin.Extensions.DependencyInjection;

public static class AddApiConfigurationExtentions
{
    public static IServiceCollection AddZaminApiCore(this IServiceCollection services, params string[] assemblyNamesForLoad)
    {
        services.AddControllers().AddFluentValidation();
        services.AddZaminDependencies(assemblyNamesForLoad);

        return services;
    }


    public static void UseZaminApiExceptionHandler(this IApplicationBuilder app)
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

    }




}