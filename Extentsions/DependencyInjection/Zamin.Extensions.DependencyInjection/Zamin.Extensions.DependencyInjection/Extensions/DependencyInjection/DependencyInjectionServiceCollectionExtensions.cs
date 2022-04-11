using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using Zamin.Extensions.DependencyInjection.Abstractions;
using Zamin.Extensions.DependencyInjection.Options;

namespace Zamin.Extensions.DependencyInjection.Extensions.DependencyInjection;

public static class DependencyInjectionServiceCollectionExtensions
{
    public static IServiceCollection AddCustomeDepenecies(this IServiceCollection services, IConfiguration configuration)
    {
        var option = configuration.Get<DependencyInjectionOption>();

        services.AddWithTransientLifetime(option.AssmblyNamesForLoad)
            .AddWithScopeLifetime(option.AssmblyNamesForLoad)
            .AddWithSingletoneLifetime(option.AssmblyNamesForLoad)
            .Configure<DependencyInjectionOption>(configuration);

        return services;
    }

    public static IServiceCollection AddCustomeDepenecies(this IServiceCollection services, IConfiguration configuration, string sectionName)
        => services.AddCustomeDepenecies(configuration.GetSection(sectionName));

    public static IServiceCollection AddCustomeDepenecies(this IServiceCollection services, Action<DependencyInjectionOption> setupAction)
    {
        var option = new DependencyInjectionOption();
        setupAction.Invoke(option);

        services.AddWithTransientLifetime(option.AssmblyNamesForLoad)
            .AddWithScopeLifetime(option.AssmblyNamesForLoad)
            .AddWithSingletoneLifetime(option.AssmblyNamesForLoad)
            .Configure(setupAction);

        return services;
    }

    private static IServiceCollection AddWithTransientLifetime(this IServiceCollection services, string assmblyNames)
        => services.Scan(s => s.FromAssemblies(GetAssemblies(assmblyNames))
        .AddClasses(c => c.AssignableToAny(typeof(ITransientLifetime)))
        .AsImplementedInterfaces()
        .WithTransientLifetime());

    private static IServiceCollection AddWithScopeLifetime(this IServiceCollection services, string assmblyNames)
        => services.Scan(s => s.FromAssemblies(GetAssemblies(assmblyNames))
        .AddClasses(c => c.AssignableToAny(typeof(IScopeLifetime)))
        .AsImplementedInterfaces()
        .WithScopedLifetime());

    private static IServiceCollection AddWithSingletoneLifetime(this IServiceCollection services, string assmblyNames)
        => services.Scan(s => s.FromAssemblies(GetAssemblies(assmblyNames))
        .AddClasses(c => c.AssignableToAny(typeof(ISingletoneLifetime)))
        .AsImplementedInterfaces()
        .WithSingletonLifetime());

    private static List<Assembly> GetAssemblies(string assmblyNames)
    {
        var assemblies = new List<Assembly>();
        var dependencies = DependencyContext.Default.RuntimeLibraries;

        foreach (var library in dependencies)
        {
            if (IsCandidateCompilationLibrary(library, assmblyNames.Split(',')))
            {
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                assemblies.Add(assembly);
            }
        }

        return assemblies;
    }

    private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary, string[] assmblyName)
        => assmblyName.Any(d => compilationLibrary.Name.Contains(d)) || compilationLibrary.Dependencies.Any(d => assmblyName.Any(c => d.Name.Contains(c)));
}
