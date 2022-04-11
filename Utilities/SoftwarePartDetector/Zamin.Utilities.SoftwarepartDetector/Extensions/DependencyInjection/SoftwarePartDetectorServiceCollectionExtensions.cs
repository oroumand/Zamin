using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Utilities.SoftwarePartDetector;
using Zamin.Utilities.SoftwarePartDetector.Options;
using Zamin.Utilities.SoftwarePartDetector.Publishers;
using Zamin.Utilities.SoftwarePartDetector.Services;

namespace Zamin.Extensions.DependencyInjection;
public static class SoftwarePartDetectorServiceCollectionExtensions
{
    public static IServiceCollection AddSoftwarePartDetector(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddHttpClient<SoftwarePartWebPublisher>();
        services.AddSingleton<ControllersAndActionDetector>();
        services.AddSingleton<SoftwarePartDetector>();
        services.AddSingleton<ISoftwarePartPublisher,SoftwarePartWebPublisher>();
        services.AddSingleton<SoftwarePartDetectorService>();
        services.Configure<SoftwarePartDetectorOptions>(configuration);
        return services;
    }
    public static IServiceCollection AddSoftwarePartDetector(this IServiceCollection services, IConfiguration configuration,string sectionName)
    {
        services.AddSoftwarePartDetector(configuration.GetSection(sectionName));
        return services;
    }
    public static IServiceCollection AddSoftwarePartDetector(this IServiceCollection services,Action<SoftwarePartDetectorOptions> setupAction)
    {
        services.AddTransient<ControllersAndActionDetector>();
        services.AddTransient<SoftwarePartDetector>();
        services.AddTransient<ISoftwarePartPublisher, SoftwarePartWebPublisher>();
        services.AddTransient<SoftwarePartDetectorService>();
        services.Configure(setupAction);
        return services;
    }
}
