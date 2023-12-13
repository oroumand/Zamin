using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Zamin.Extensions.Caching.Abstractions;
using Zamin.Utilities.Auth.ApiAuthentication.Models;
using Zamin.Utilities.Auth.ApiAuthentication.Options;

namespace Zamin.Extensions.DependencyInjection;

public static class UtilitiesExtensions
{
    public static string AddProviderHttpClient(this ProviderOption provider, IServiceCollection services)
    {
        var httpClientName = string.IsNullOrWhiteSpace(provider.HttpClientFactoryName) ? $"Provider{provider.Priority}HttpClient" : provider.HttpClientFactoryName;

        IHttpClientBuilder httpClientBuilder = services.AddHttpClient(httpClientName, option => { option.BaseAddress = new Uri(provider.Authority); });
        if (provider.IgnoreSSL)
        {
            httpClientBuilder.ConfigurePrimaryHttpMessageHandler(
                () => new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = delegate { return true; }
                });
        }

        return httpClientName;
    }

    public static async Task<List<Claim>> GetUserInfoClaims(this ProviderOption provider, HttpContext httpContext, string httpClientName)
    {
        var token = httpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "")
            ?? throw new ArgumentNullException($"{provider.Scheme} ({provider.Authority}) , token is null");

        var client = httpContext.RequestServices.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientName);

        var userInfoClaims = await provider.UserInfoEndpointCaller(httpContext, client, token);

        return userInfoClaims;
    }

    public static async Task<List<Claim>> UserInfoEndpointCaller(this ProviderOption provider, HttpContext httpContext, HttpClient client, string token)
    {

        List<Claim> claims = [];
        if (provider.RegisterUserInfoClaims.CachingData)
        {
            var cacheAdapter = httpContext.RequestServices.GetRequiredService<ICacheAdapter>()
                ?? throw new ArgumentNullException($"{provider.Scheme} ({provider.Authority}) , cache adapter is null");

            string cacheKey = GenerateCacheKey(provider, token);

            claims = cacheAdapter.Get<List<ClaimCacheModel>>(cacheKey)?.Select(cacheModel => cacheModel.ToClaim()).ToList() ?? [];
            if (claims is null || claims.Count == 0)
            {
                claims = await provider.CallUserInfoEndpoint(client, token);
                switch (provider.RegisterUserInfoClaims.CacheExpirationType)
                {
                    case CacheExpirationType.Absolute:
                        cacheAdapter.Add(cacheKey,
                                         claims.Select(ClaimCacheModel.FromClaim),
                                         DateTime.Now.AddSeconds(provider.RegisterUserInfoClaims.CacheExpirationInSeconds),
                                         null);
                        break;
                    case CacheExpirationType.Sliding:
                        cacheAdapter.Add(cacheKey,
                                         claims.Select(ClaimCacheModel.FromClaim),
                                         null,
                                         TimeSpan.FromSeconds(provider.RegisterUserInfoClaims.CacheExpirationInSeconds));
                        break;
                    default:
                        throw new InvalidOperationException($"Invalid cache expiration type for {provider.Scheme} ({provider.Authority})");
                }
            }
        }
        else
        {
            claims = await provider.CallUserInfoEndpoint(client, token);
        }

        return claims;
    }

    private static string GenerateCacheKey(ProviderOption provider, string token)
    {
        var cacheKeyText = $"{provider.RegisterUserInfoClaims.CacheKeyPrefix}{provider.Scheme}_{token}";
        var cacheKey = provider.RegisterUserInfoClaims.CacheKeyFormat == CacheKeyFormat.Base64 ? cacheKeyText.ToBase64Encode() : cacheKeyText;
        return cacheKey;
    }

    public static async Task<List<Claim>> CallUserInfoEndpoint(this ProviderOption provider, HttpClient client, string token)
    {
        var response = await client.GetUserInfoAsync(new UserInfoRequest
        {
            Address = provider.EndpointsPath?.UserInfoEndpoint ?? (await client.GetDiscoveryDocumentAsync())?.UserInfoEndpoint?.Replace(provider.Authority, ""),
            Token = token
        });

        if (response.IsError) throw new Exception(response.Error);

        return [.. response.Claims];
    }

    public static ClaimsIdentity CreateClaimsIdentity(this ClaimsPrincipal? principal, List<Claim> claims)
        => new(principal?.Claims?.ToList().GetNotExist([.. claims]),
               principal?.Identities.FirstOrDefault()?.AuthenticationType,
               principal?.Identities.FirstOrDefault()?.NameClaimType,
               principal?.Identities.FirstOrDefault()?.RoleClaimType);

    public static ClaimsPrincipal? ClonePrincipalWithConvertedClaims(this ClaimsPrincipal? principal, ProviderOption provider)
    {
        if (principal is null) return null;

        ClaimsPrincipal clone = principal.Clone();

        List<Claim>? claims = provider.ClaimConvertor([.. clone.Claims]);
        string? authenticationType = clone.Identities.First().AuthenticationType;
        string? nameType = clone.Identities.First().NameClaimType;
        string? roleType = clone.Identities.First().RoleClaimType;
        ClaimsIdentity claimsIdentity = new(claims, authenticationType, nameType, roleType);

        return new(claimsIdentity);
    }

    public static List<Claim> ClaimConvertor(this ProviderOption provider, List<Claim> currentClaims)
    {
        List<Claim> convertedClaima = [];

        currentClaims.ForEach((currentClaim) =>
        {
            var mapRule = provider.UserClaimConvertTypeRules.FirstOrDefault(c => c.Source.Equals(currentClaim.Type));

            convertedClaima.Add(mapRule is null ? currentClaim
                : new Claim(mapRule.Destination,
                            currentClaim.Value,
                            currentClaim.ValueType,
                            currentClaim.Issuer,
                            currentClaim.OriginalIssuer,
                            currentClaim.Subject));

        });

        return convertedClaima;
    }

    public static List<Claim> GetNotExist(this List<Claim> current, List<Claim> target)
        => [.. target.Where(claim => !current.Any(currentClaim => currentClaim.Type.Equals(claim.Type) && currentClaim.Value.Equals(claim.Value)))];

    private static string ToBase64Encode(this string plainText) => Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainText));
}