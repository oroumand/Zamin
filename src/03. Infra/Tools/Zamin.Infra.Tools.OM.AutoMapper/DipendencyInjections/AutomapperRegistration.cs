using AutoMapper;
using Zamin.Infra.Tools.OM.AutoMapper.Common;
using Zamin.Utilities.Services.ObjectMappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Zamin.Infra.Tools.OM.AutoMapper.DipendencyInjections
{
    public static class AutomapperRegistration
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            var profileTypes = assemblies
                .SelectMany(x => x.DefinedTypes)
                .Where(type => typeof(Profile).IsAssignableFrom(type)).ToList();
            var profiles = new List<Profile>();
            foreach (var profileType in profileTypes)
            {
                profiles.Add((Profile)Activator.CreateInstance(profileType));
            }
            return services.AddSingleton<IMapperAdapter>(new AutoMapperAdapter(profiles.ToArray()));
        }
    }
}
