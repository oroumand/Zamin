using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Utilities.Auth.ApiAuthentication.Options;

namespace Zamin.Extensions.DependencyInjection;

public static class ApiAuthenticationServiceCollectionExtensions
{
    public static IServiceCollection AddZaminApiAuthentication(this IServiceCollection services, IConfiguration configuration, string sectionName)
        => services.AddZaminApiAuthentication(configuration.GetSection(sectionName));

    public static IServiceCollection AddZaminApiAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiAuthenticationOption>(configuration);
        var option = configuration.Get<ApiAuthenticationOption>() ?? new();

        return services.AddAuthentication(option);
    }

    public static IServiceCollection AddZaminApiAuthentication(this IServiceCollection services, Action<ApiAuthenticationOption> action)
    {
        services.Configure(action);
        var option = new ApiAuthenticationOption();
        action.Invoke(option);

        return services.AddAuthentication(option);
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, ApiAuthenticationOption option)
    {
        if (option.Active)
        {
            ProviderOption defualt = option.DefualtProvider ?? throw new InvalidOperationException($"DefualtProvider is null");

            var authenticationBuilder = services.AddAuthentication(defualt.Scheme);

            for (var i = 0; i < option.EnabledProviders.Count; i++)
            {
                ProviderOption provider = option.EnabledProviders[i];

                switch (provider.TokenTypeSupport)
                {
                    case TokenType.Jwt:
                        authenticationBuilder.AddJwtTokenSupoort(services, provider);
                        break;

                    case TokenType.Reference:
                        authenticationBuilder.AddReferenceTokenSupoort(services, provider);
                        break;

                    default:
                        throw new InvalidOperationException($"Invalid token type for {provider.Scheme} ({provider.Authority})");
                }
            }

            services.AddAuthorizationBuilder()
                .SetDefaultPolicy(new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes([.. option.EnabledProviders.Select(c => c.Scheme)])
                    .Build());
        }

        return services;
    }
}