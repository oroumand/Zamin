using Microsoft.OpenApi.Models;

namespace Zamin.Utilities.Swagger.Registration.Options;

public class SwaggerSecurityFlowOption
{
    public bool Enabled { get; set; } = true;
    public string AuthorizationUrl { get; set; } = "/connect/authorize";
    public string TokenUrl { get; set; } = "/connect/token";
    public string? RefreshUrl { get; set; }
    public List<SwaggerSecurityFlowScopeOption> Scopes { get; set; } = [];

    public OpenApiOAuthFlow ToOpenApiOAuthFlow(SwaggerSecurityOption swaggerSecurityOption)
    {
        if (string.IsNullOrWhiteSpace(AuthorizationUrl))
            throw new ArgumentNullException($"SwaggerSecurity{swaggerSecurityOption.Priority} AuthorizationUrl is null.");

        if (string.IsNullOrWhiteSpace(TokenUrl))
            throw new ArgumentNullException($"SwaggerSecurity{swaggerSecurityOption.Priority} TokenUrl is null.");

        OpenApiOAuthFlow flow = new()
        {
            AuthorizationUrl = new(swaggerSecurityOption.OpenIdConnectUrl + AuthorizationUrl),
            TokenUrl = new(swaggerSecurityOption.OpenIdConnectUrl + TokenUrl),
            Scopes = Scopes.Where(scope => scope.Enabled).Select(scope => scope.ToKeyValuePair(swaggerSecurityOption.Priority)).ToDictionary()
        };

        if (!string.IsNullOrWhiteSpace(RefreshUrl))
        {
            flow.RefreshUrl = new(RefreshUrl);
        }

        return flow;
    }
}