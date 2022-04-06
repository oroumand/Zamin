using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Zamin.Extentions.ObjectMappers.Abstractions;
using Zamin.Extentions.ObjectMappers.AutoMapper.Services;

namespace Zamin.Extentions.ObjectMappers.AutoMapper.Extensions.DependencyInjection;

public static class AutoMapperServiceCollectionExtensions
{
    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        var profileTypes = assemblies
            .SelectMany(x => x.DefinedTypes)
            .Where(type => typeof(Profile).IsAssignableFrom(type)).ToList();
        var profiles = new List<Profile>();
        foreach (var profileType in profileTypes)
        {
            if (Activator.CreateInstance(profileType) is Profile profile)
                profiles.Add(profile);
        }
        return services.AddSingleton<IMapperAdapter>(new AutoMapperAdapter(profiles.ToArray()));
    }
}
