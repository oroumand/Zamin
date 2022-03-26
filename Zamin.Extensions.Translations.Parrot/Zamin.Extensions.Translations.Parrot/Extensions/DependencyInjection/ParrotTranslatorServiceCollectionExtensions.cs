using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.Translations.Parrot.Options;
using Zamin.Extensions.Translations.Parrot.Services;
using Zamin.Extentions.Translations.Abstractions;

namespace Zamin.Extensions.Translations.Parrot.Extensions.DependencyInjection;

public static class ParrotTranslatorServiceCollectionExtensions
{
    public static IServiceCollection AddParrotTranslator(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ITranslator, ParrotTranslator>();
        services.Configure<ParrotTranslatorOptions>(configuration);
        return services;
    }

    public static IServiceCollection AddParrotTranslator(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddParrotTranslator(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddParrotTranslator(this IServiceCollection services, Action<ParrotTranslatorOptions> setupAction)
    {
        services.AddSingleton<ITranslator, ParrotTranslator>();
        services.Configure(setupAction);
        return services;
    }
}