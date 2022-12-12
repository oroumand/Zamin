using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.Translations.Parrot.Options;
using Zamin.Extensions.Translations.Parrot.Services;
using Zamin.Extensions.Translations.Abstractions;

namespace Zamin.Extensions.DependencyInjection;

public static class ParrotTranslatorServiceCollectionExtensions
{
    public static IServiceCollection AddZaminParrotTranslator(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ITranslator, ParrotTranslator>();
        services.Configure<ParrotTranslatorOptions>(configuration);
        return services;
    }

    public static IServiceCollection AddZaminParrotTranslator(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddZaminParrotTranslator(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddZaminParrotTranslator(this IServiceCollection services, Action<ParrotTranslatorOptions> setupAction)
    {
        services.AddSingleton<ITranslator, ParrotTranslator>();
        services.Configure(setupAction);
        return services;
    }
}