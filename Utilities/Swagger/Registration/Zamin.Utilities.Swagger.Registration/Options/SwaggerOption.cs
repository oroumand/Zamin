namespace Zamin.Utilities.Swagger.Registration.Options;

public class SwaggerOption
{
    public bool Enabled { get; set; } = true;
    public string Name { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Version { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string RoutePrefix { get; set; } = string.Empty;
    public SwaggerOAuthConfigOption OAuthConfig { get; set; } = new();
    public List<SwaggerSecurityOption> Securities { get; set; } = [];
    public List<SwaggerSecurityOption> EnabledSecurities
        => Securities.Where(security => security.Enabled).OrderBy(security => security.Priority).ToList();
}