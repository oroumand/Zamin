using Zamin.CqrsRouter.HandlerRegistrations.Registration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Zamin.CqrsRouter.HandlerRegistrations
{
    public static class CqRouterServiceCollectionExtensions
    {
        /// <summary>
        /// Registers handlers and mediator types from the specified assemblies
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="assemblies">Assemblies to scan</param>        
        /// <returns>Service collection</returns>
        public static IServiceCollection AddCqRouter(this IServiceCollection services, params Assembly[] assemblies)
            => services.AddCqRouter(assemblies, configuration: null);

        /// <summary>
        /// Registers handlers and mediator types from the specified assemblies
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="assemblies">Assemblies to scan</param>
        /// <param name="configuration">The action used to configure the options</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddCqRouter(this IServiceCollection services, Action<CqRouterServiceConfiguration> configuration, params Assembly[] assemblies)
            => services.AddCqRouter(assemblies, configuration);

        /// <summary>
        /// Registers handlers and mediator types from the specified assemblies
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="assemblies">Assemblies to scan</param>
        /// <param name="configuration">The action used to configure the options</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddCqRouter(this IServiceCollection services, IEnumerable<Assembly> assemblies, Action<CqRouterServiceConfiguration> configuration)
        {
            if (!assemblies.Any())
            {
                throw new ArgumentException("No assemblies found to scan. Supply at least one assembly to scan for handlers.");
            }
            var serviceConfig = new CqRouterServiceConfiguration();

            configuration?.Invoke(serviceConfig);

            CqRouterServiceRegistrar.AddRequiredServices(services, serviceConfig);

            CqRouterServiceRegistrar.AddCqRouterClasses(services, assemblies);

            return services;
        }

        /// <summary>
        /// Registers handlers and mediator types from the assemblies that contain the specified types
        /// </summary>
        /// <param name="services"></param>
        /// <param name="handlerAssemblyMarkerTypes"></param>        
        /// <returns>Service collection</returns>
        public static IServiceCollection AddCqRouter(this IServiceCollection services, params Type[] handlerAssemblyMarkerTypes)
            => services.AddCqRouter(handlerAssemblyMarkerTypes, configuration: null);

        /// <summary>
        /// Registers handlers and mediator types from the assemblies that contain the specified types
        /// </summary>
        /// <param name="services"></param>
        /// <param name="handlerAssemblyMarkerTypes"></param>
        /// <param name="configuration">The action used to configure the options</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddCqRouter(this IServiceCollection services, Action<CqRouterServiceConfiguration> configuration, params Type[] handlerAssemblyMarkerTypes)
            => services.AddCqRouter(handlerAssemblyMarkerTypes, configuration);

        /// <summary>
        /// Registers handlers and mediator types from the assemblies that contain the specified types
        /// </summary>
        /// <param name="services"></param>
        /// <param name="handlerAssemblyMarkerTypes"></param>
        /// <param name="configuration">The action used to configure the options</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddCqRouter(this IServiceCollection services, IEnumerable<Type> handlerAssemblyMarkerTypes, Action<CqRouterServiceConfiguration> configuration)
            => services.AddCqRouter(handlerAssemblyMarkerTypes.Select(t => t.GetTypeInfo().Assembly), configuration);
    }
}
