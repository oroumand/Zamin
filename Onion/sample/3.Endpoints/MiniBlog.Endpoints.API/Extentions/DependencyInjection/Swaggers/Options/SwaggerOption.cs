namespace MiniBlog.Endpoints.API.Extentions.DependencyInjection.Swaggers.Options;

public class SwaggerOption
{
    public bool Enabled { get; set; } = true;
    public SwaggerDocOption SwaggerDoc { get; set; } = new();
    public SwaggerOAuthOption OAuth { get; set; } = new();
}

public class SwaggerDocOption
{
    public string Version { get; set; } = "v1";
    public string Title { get; set; } = string.Empty;
    public string Name { get; set; } = "v1";
    public string URL { get; set; } = "/swagger/v1/swagger.json";
}

public class SwaggerOAuthOption
{
    public bool Enabled { get; set; } = false;
    public string AuthorizationUrl { get; set; } = string.Empty;
    public string TokenUrl { get; set; } = string.Empty;
    public Dictionary<string, string> Scopes { get; set; } = new();
}