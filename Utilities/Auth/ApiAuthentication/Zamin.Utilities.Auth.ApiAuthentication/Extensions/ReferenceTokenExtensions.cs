using IdentityModel.AspNetCore.OAuth2Introspection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Zamin.Utilities.Auth.ApiAuthentication.Extensions;
using Zamin.Utilities.Auth.ApiAuthentication.Options;

namespace Zamin.Extensions.DependencyInjection;

public static class ReferenceTokenExtensions
{
    public static AuthenticationBuilder AddReferenceTokenSupoort(this AuthenticationBuilder authenticationBuilder,
                                                                 IServiceCollection services,
                                                                 ProviderOption provider)
    {
        if (string.IsNullOrWhiteSpace(provider.RefrenceTokenConfig.ClientId) || string.IsNullOrWhiteSpace(provider.RefrenceTokenConfig.ClientSecret))
            throw new ArgumentNullException($"{provider.Scheme} ({provider.Authority}) , ClientId or ClientSecret is null or white space or empty");

        string httpClientName = provider.AddProviderHttpClient(services);

        authenticationBuilder.AddOAuth2Introspection(provider.Scheme, option =>
        {
            option.Authority = provider.Authority;

            if (!string.IsNullOrEmpty(provider.EndpointsPath?.IntrospectionEndpoint))
            {
                option.IntrospectionEndpoint = $"{provider.Authority}{provider.EndpointsPath.IntrospectionEndpoint}";
            }

            option.ClientId = provider.RefrenceTokenConfig.ClientId;
            option.ClientSecret = provider.RefrenceTokenConfig.ClientSecret;

            option.Events = new OAuth2IntrospectionEvents()
            {
                OnTokenValidated = async (context) =>
                {
                    if (context.Principal is null) throw new ArgumentNullException($"{provider.Scheme} ({provider.Authority}) , principal is null");

                    if (provider.RegisterUserInfoClaims.Enabled && context.Principal.HasSubClaim(provider.UserIdentifierClaimType))
                    {
                        List<Claim> claims = await provider.GetUserInfoClaims(context.HttpContext, httpClientName);
                        context.Principal.AddIdentity(context.Principal.CreateClaimsIdentity(claims));
                    }

                    if (provider.UserClaimConvertTypeRules.Count != 0)
                    {
                        context.Principal = context.Principal.ClonePrincipalWithConvertedClaims(provider);
                    }
                },
            };
        });

        return authenticationBuilder;
    }
}