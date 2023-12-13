using System.Security.Claims;

namespace Zamin.Utilities.Auth.ApiAuthentication.Options;

public class RegisterUserInfoClaimsOption
{
    public bool Enabled { get; set; } = true;
    public string UserIdentifierClaimType { get; set; } = ClaimTypes.NameIdentifier;
    public bool CachingData { get; set; } = false;
    public string CacheKeyPrefix { get; set; } = "UserInfoClaims_";
    public CacheKeyFormat CacheKeyFormat { get; set; } = CacheKeyFormat.Base64;
    public CacheExpirationType CacheExpirationType { get; set; } = CacheExpirationType.Absolute;
    public int CacheExpirationInSeconds { get; set; } = 60;
}