using System.Security.Claims;
using Zamin.Utilities.Auth.ApiAuthentication.Options;

namespace Zamin.Utilities.Auth.ApiAuthentication.Extensions;

public static class ClaimsPrincipalExtentions
{
    public static bool HasSubClaim(this ClaimsPrincipal? principal, string userIdentifierClaimType)
        => principal?.Claims.Any(c => c.Type.Equals(userIdentifierClaimType)) ?? false;

    public static ClaimsIdentity CreateClaimsIdentity(this ClaimsPrincipal? principal, List<Claim> claims)
        => new(principal?.Claims?.ToList().GetNotExist([.. claims]),
               principal?.Identities.FirstOrDefault()?.AuthenticationType,
               principal?.Identities.FirstOrDefault()?.NameClaimType,
               principal?.Identities.FirstOrDefault()?.RoleClaimType);

    public static ClaimsPrincipal? ClonePrincipalWithConvertedClaims(this ClaimsPrincipal? principal, ProviderOption provider)
    {
        if (provider.UserClaimRules.Any(rule => string.IsNullOrWhiteSpace(rule.Source) || string.IsNullOrWhiteSpace(rule.Destination)))
            throw new ArgumentNullException("Source or Destination can not be null or white-space in UserClaimRule");

        if (provider.UserClaimAddons.Any(addon => string.IsNullOrWhiteSpace(addon.Type) || string.IsNullOrWhiteSpace(addon.Value)))
            throw new ArgumentNullException("Type or Value can not be null or white-space in UserClaimAddon");

        if (principal is null) return null;

        ClaimsPrincipal clone = principal.Clone();

        List<Claim> claims = [];
        claims.AddRange(provider.UserClaimRulesProcesor([.. clone.Claims]));
        claims.AddRange(provider.UserClaimAddonsProcesor());

        string? authenticationType = clone.Identities.First().AuthenticationType;
        string? nameType = clone.Identities.First().NameClaimType;
        string? roleType = clone.Identities.First().RoleClaimType;

        ClaimsIdentity claimsIdentity = new(claims, authenticationType, nameType, roleType);

        return new(claimsIdentity);
    }

    private static List<Claim> UserClaimRulesProcesor(this ProviderOption provider, List<Claim> currentClaims)
    {
        List<Claim> newClaims = [];

        provider.UserClaimRules.ForEach(rule =>
        {
            var currentClaim = currentClaims.FirstOrDefault(claim => claim.Type.Equals(rule.Source));

            if (currentClaim is not null)
            {
                var mappedClaim = new Claim(rule.Destination,
                                            currentClaim.Value,
                                            currentClaim.ValueType,
                                            currentClaim.Issuer,
                                            currentClaim.OriginalIssuer,
                                            currentClaim.Subject);

                newClaims.Add(mappedClaim);

                if (rule.RemoveSource is false)
                {
                    newClaims.Add(currentClaim);
                }
            }
        });

        return newClaims;
    }

    private static List<Claim> UserClaimAddonsProcesor(this ProviderOption provider)
        => provider.UserClaimAddons.Select(addon => new Claim(addon.Type,
                                                              addon.Value,
                                                              addon.ValueType,
                                                              addon.Issuer,
                                                              addon.OriginalIssuer)).ToList();

    private static List<Claim> GetNotExist(this List<Claim> current, List<Claim> target)
        => [.. target.Where(claim => !current.Any(currentClaim => currentClaim.Type.Equals(claim.Type) && currentClaim.Value.Equals(claim.Value)))];
}
