namespace Zamin.Utilities.Auth.ApiAuthentication.Options;

public class JwtTokenConfigOption
{
    public string? Audience { get; set; } = null;
    public bool RequireHttpsMetadata { get; set; } = false;
    public bool ValidateAudience { get; set; } = false;
    public bool ValidateIssuer { get; set; } = false;
    public bool ValidateIssuerSigningKey { get; set; } = false;
}
