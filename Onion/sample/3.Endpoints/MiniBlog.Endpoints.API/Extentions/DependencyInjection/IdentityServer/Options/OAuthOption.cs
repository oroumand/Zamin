namespace MiniBlog.Endpoints.API.Extentions.DependencyInjection.IdentityServer.Options;

public class OAuthOption
{
    public bool Enabled { get; set; } = false;
    public string Authority { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public bool RequireHttpsMetadata { get; set; } = false;
    public Dictionary<string, string> Scopes { get; set; } = new Dictionary<string, string>();

    public bool ValidateAudience { get; set; } = false;
    public bool ValidateIssuer { get; set; } = false;
    public bool ValidateIssuerSigningKey { get; set; } = false;
}
