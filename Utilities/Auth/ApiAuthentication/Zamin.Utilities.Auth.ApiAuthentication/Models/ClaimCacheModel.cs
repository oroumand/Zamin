using System.Security.Claims;

namespace Zamin.Utilities.Auth.ApiAuthentication.Models;

public class ClaimCacheModel
{
    public string Type { get; set; }
    public string Value { get; set; }
    public string? ValueType { get; set; }
    public string? Issuer { get; set; }
    public string? OriginalIssuer { get; set; }
    public ClaimsIdentity? Subject { get; set; }

    public ClaimCacheModel() { }

    public ClaimCacheModel(string type, string value, string? valueType, string? issuer, string? originalIssuer, ClaimsIdentity? subject) : this()
    {
        Type = type;
        Value = value;
        ValueType = valueType;
        Issuer = issuer;
        OriginalIssuer = originalIssuer;
        Subject = subject;
    }

    public static ClaimCacheModel FromClaim(Claim claim) => new(claim.Type, claim.Value, claim.ValueType, claim.Issuer, claim.OriginalIssuer, claim.Subject);

    public Claim ToClaim() => new(Type, Value, ValueType, Issuer, OriginalIssuer, Subject);
}
