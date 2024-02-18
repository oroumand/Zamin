namespace Zamin.Utilities.Auth.ApiAuthentication.Options;

public class UserClaimAddonOption
{
    public string Type { get; set; } = default!;
    public string Value { get; set; } = default!;
    public string? ValueType { get; set; }
    public string? Issuer { get; set; }
    public string? OriginalIssuer { get; set; }
}