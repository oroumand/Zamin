using System.Security.Claims;
using Zamin.Utilities.Auth.ApiAuthentication.Options;

namespace Zamin.Utilities.Auth.ApiAuthentication.Extensions;

public static class ClaimsPrincipalExtentions
{
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

    public static bool HasSubClaim(this ClaimsPrincipal? principal, string userIdentifierClaimType)
        => principal?.Claims.Any(c => c.Type.Equals(userIdentifierClaimType)) ?? false;

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

    private static List<Claim> GetNotExist(this List<Claim> current, List<Claim> target)
        => [.. target.Where(claim => !current.Any(currentClaim => currentClaim.Type.Equals(claim.Type) && currentClaim.Value.Equals(claim.Value)))];
}
