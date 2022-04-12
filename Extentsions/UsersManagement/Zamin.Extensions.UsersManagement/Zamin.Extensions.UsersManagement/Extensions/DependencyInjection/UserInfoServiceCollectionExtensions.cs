using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.UsersManagement.Services;
using Zamin.Extentions.UsersManagement.Abstractions;

namespace Zamin.Extensions.DependencyInjection;

public static class UserInfoServiceCollectionExtensions
{
    public static IServiceCollection AddWebUserInfoService(this IServiceCollection services,bool useFake=false)
    {
        if(useFake)
        {
            services.AddSingleton<IUserInfoService, FakeUserInfoService>();

        }
        else
        {
            services.AddSingleton<IUserInfoService, WebUserInfoService>();

        }
        return services;
    }
}

