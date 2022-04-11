using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.UsersManagement.Services;
using Zamin.Extentions.UsersManagement.Abstractions;

namespace Zamin.Extensions.UsersManagement.Extensions.DependencyInjection;

public static class UserInfoServiceCollectionExtensions
{
    public static IServiceCollection AddWebUserInfoService(this IServiceCollection services)
    {
        services.AddSingleton<IUserInfoService, WebUserInfoService>();
        return services;
    }

    public static IServiceCollection AddFakeUserInfoService(this IServiceCollection services)
    {
        services.AddSingleton<IUserInfoService, FakeUserInfoService>();
        return services;
    }
}

