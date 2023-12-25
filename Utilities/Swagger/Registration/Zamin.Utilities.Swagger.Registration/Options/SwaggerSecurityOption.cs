using Microsoft.OpenApi.Models;

namespace Zamin.Utilities.Swagger.Registration.Options;

public class SwaggerSecurityOption
{
    public bool Enabled { get; set; } = true;
    public string Scheme { get; set; } = default!;
    public SecuritySchemeType Type { get; set; } = SecuritySchemeType.OAuth2;
    public string Name { get; set; } = "Authorization";
    public ParameterLocation In { get; set; } = ParameterLocation.Header;
    public string Description { get; set; } = "Bearer {access_token}";
    public string BearerFormat { get; set; } = "JWT";
    public string OpenIdConnectUrl { get; set; } = default!;
    public SwaggerSecurityFlowOption? AuthorizationCodeFlow { get; set; } = null;
    public SwaggerSecurityFlowOption? ClientCredentialsFlow { get; set; } = null;
    public int Priority { get; set; } = 1;

    public OpenApiSecurityScheme ToOpenApiSecurityScheme()
    {
        if (string.IsNullOrWhiteSpace(OpenIdConnectUrl))
            throw new ArgumentNullException($"SwaggerSecurity{Priority} OpenIdConnectUrl is null.");

        if (string.IsNullOrWhiteSpace(Scheme))
            throw new ArgumentNullException($"SwaggerSecurity{Priority} Scheme is null.");

        if (string.IsNullOrWhiteSpace(Name))
            throw new ArgumentNullException($"SwaggerSecurity{Priority} Name is null.");

        if (AuthorizationCodeFlow is null && ClientCredentialsFlow is null)
            throw new ArgumentNullException($"SwaggerSecurity{Priority} AuthorizationCodeFlow And ClientCredentialsFlow are null.");

        OpenApiSecurityScheme security = new()
        {
            OpenIdConnectUrl = new(OpenIdConnectUrl),
            Scheme = Scheme,
            Type = Type,
            Name = Name,
            In = In,
            Description = Description,
            BearerFormat = BearerFormat,
            Flows = new()
        };

        if (AuthorizationCodeFlow is not null && AuthorizationCodeFlow.Enabled)
            security.Flows.AuthorizationCode = AuthorizationCodeFlow.ToOpenApiOAuthFlow(this);

        if (ClientCredentialsFlow is not null && ClientCredentialsFlow.Enabled)
            security.Flows.ClientCredentials = ClientCredentialsFlow.ToOpenApiOAuthFlow(this);

        return security;
    }

    public OpenApiSecurityRequirement ToOpenApiSecurityRequirement()
    {
        if (string.IsNullOrWhiteSpace(Scheme))
            throw new ArgumentNullException($"SwaggerSecurity{Priority} Scheme is null.");

        return new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = Scheme
                    }
                },
                new List<string>()
            }
        };
    }
}