using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using Zamin.Extentions.ObjectMappers.Abstractions;
using Zamin.Extentions.ObjectMappers.AutoMapper.Options;
using Zamin.Extentions.ObjectMappers.AutoMapper.Services;

namespace Zamin.Extentions.ObjectMappers.AutoMapper.Extensions.DependencyInjection;

public static class AutoMapperServiceCollectionExtensions
{
    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services,
                                                          IConfiguration configuration,
                                                          string sectionName)
        => services.AddAutoMapperProfiles(configuration.GetSection(sectionName));

    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services, IConfiguration configuration)
    {
        var option = configuration.Get<AutoMapperOption>();

        var assemblies = GetAssemblies(option.AssmblyNamesForLoadProfiles);

        return services.AddAutoMapper(assemblies).AddSingleton<IMapperAdapter, AutoMapperAdapter>();
    }

    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services, Action<AutoMapperOption> setupAction)
    {
        var option = new AutoMapperOption();
        setupAction.Invoke(option);

        var assemblies = GetAssemblies(option.AssmblyNamesForLoadProfiles);

        return services.AddAutoMapper(assemblies).AddSingleton<IMapperAdapter, AutoMapperAdapter>();
    }

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
        => assmblyName.Any(d => compilationLibrary.Name.Contains(d))
           || compilationLibrary.Dependencies.Any(d => assmblyName.Any(c => d.Name.Contains(c)));
}