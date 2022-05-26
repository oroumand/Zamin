using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.ObjectMappers.AutoMapper.Options;
using Zamin.Extensions.UsersManagement.Services;
using Zamin.Extentions.UsersManagement.Abstractions;

namespace Zamin.Extensions.DependencyInjection;

public static class UserInfoServiceCollectionExtensions
{
    public static IServiceCollection AddZaminWebUserInfoService(this IServiceCollection services)
        => services.AddSingleton<IUserInfoService, WebUserInfoService>();

    public static IServiceCollection AddFakeZaminWebUserInfoService(this IServiceCollection services)
        => services.AddSingleton<IUserInfoService, FakeUserInfoService>();

    public static IServiceCollection AddSystemWorkerUserInfoService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IUserInfoService, SystemWorkerUserInfoService>();
        services.Configure<SystemWorkerUserInfoOption>(configuration);
        return services;
    }

    public static IServiceCollection AddSystemWorkerUserInfoService(this IServiceCollection services, IConfiguration configuration, string sectionName) 
        => services.AddSystemWorkerUserInfoService(configuration.GetSection(sectionName));

    public static IServiceCollection AddSystemWorkerUserInfoService(this IServiceCollection services, Action<SystemWorkerUserInfoOption> setupAction)
    {
        services.AddSingleton<IUserInfoService, SystemWorkerUserInfoService>();
        services.Configure(setupAction);
        return services;
    }
}