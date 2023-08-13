using MiniBlog.Endpoints.API.Extentions.DependencyInjection.IdentityServer.Options;

namespace MiniBlog.Endpoints.API.Extentions.DependencyInjection.IdentityServer.Extentions;

public static class IdentityServerServiceExtension
{
    public static IServiceCollection AddIdentityServer(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        var oAuthOption = configuration.GetSection(sectionName).Get<OAuthOption>();

        if (oAuthOption != null && oAuthOption.Enabled)
        {
            services.AddAuthentication()
                .AddJwtBearer(o =>
                {
                    o.Authority = oAuthOption.Authority;
                    o.Audience = oAuthOption.Audience;
                    o.RequireHttpsMetadata = oAuthOption.RequireHttpsMetadata;
                    o.BackchannelHttpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = delegate { return true; } };
                    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateAudience = oAuthOption.ValidateAudience,
                        ValidateIssuer = oAuthOption.ValidateIssuer,
                        ValidateIssuerSigningKey = oAuthOption.ValidateIssuerSigningKey
                    };
                });
        }

        return services;
    }

    public static bool UseIdentityServer(this WebApplication app, string sectionName)
    {
        var oAuthOption = app.Configuration.GetSection(sectionName).Get<OAuthOption>();

        if (oAuthOption != null && oAuthOption.Enabled)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }

        return oAuthOption != null && oAuthOption.Enabled;
    }

}
