using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.ChangeDataLog.Abstractions;
using Zamin.Extensions.ChangeDataLog.Sql;
using Zamin.Extensions.ChangeDataLog.Sql.Options;

namespace Zamin.Extensions.DependencyInjection;

public static class ChageDatalogServiceCollectionExtensions
{
    public static IServiceCollection AddZaminChageDatalogDalSql(this IServiceCollection services, IConfiguration configuration)
    {        
        services.AddScoped<IEntityChageInterceptorItemRepository, DapperEntityChangeInterceptorItemRepository>();
        services.Configure<ChangeDataLogSqlOptions>(configuration);
        return services;
    }

    public static IServiceCollection AddZaminChageDatalogDalSql(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddZaminChageDatalogDalSql(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddZaminChageDatalogDalSql(this IServiceCollection services, Action<ChangeDataLogSqlOptions> setupAction)
    {
        services.AddScoped<IEntityChageInterceptorItemRepository, DapperEntityChangeInterceptorItemRepository>();
        services.Configure(setupAction);
        return services;
    }
}