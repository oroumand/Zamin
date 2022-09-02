using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Utilities.SoftwarePartDetector;
using Zamin.Utilities.SoftwarePartDetector.Authentications;
using Zamin.Utilities.SoftwarePartDetector.Options;
using Zamin.Utilities.SoftwarePartDetector.Publishers;
using Zamin.Utilities.SoftwarePartDetector.Services;

namespace Zamin.Extensions.DependencyInjection;
public static class SoftwarePartDetectorServiceCollectionExtensions
{
    public static IServiceCollection AddSoftwarePartDetector(this IServiceCollection services, IConfiguration configuration, string sectionName)
        => services.AddSoftwarePartDetector(configuration.GetSection(sectionName));

    public static IServiceCollection AddSoftwarePartDetector(this IServiceCollection services, IConfiguration configuration)
        => services.AddServices(configuration.Get<SoftwarePartDetectorOptions>()).Configure<SoftwarePartDetectorOptions>(configuration);

    public static IServiceCollection AddSoftwarePartDetector(this IServiceCollection services, Action<SoftwarePartDetectorOptions> setupAction)
    {
        var option = new SoftwarePartDetectorOptions();

        setupAction.Invoke(option);

        return services.AddServices(option).Configure(setupAction);
    }

    private static IServiceCollection AddServices(this IServiceCollection services, SoftwarePartDetectorOptions option)
    {
        services.AddTransient<ControllersAndActionDetector>();
        services.AddTransient<SoftwarePartDetector>();
        services.AddTransient<ISoftwarePartPublisher, SoftwarePartWebPublisher>();
        services.AddTransient<ISoftwarePartAuthentication, SoftwarePartAuthentication>();
        services.AddTransient<SoftwarePartDetectorService>();

        var publisherHttpClient = services.AddHttpClient<ISoftwarePartPublisher, SoftwarePartWebPublisher>();
        var oidcHttpClient = services.AddHttpClient<ISoftwarePartAuthentication, SoftwarePartAuthentication>();

        if (option.FakeSSL)
        {
            publisherHttpClient.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true });
            oidcHttpClient.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true });
        }

        return services;
    }
}