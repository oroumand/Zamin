using FluentValidation;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.ApplicationServices.Events;
using Zamin.Core.Contracts.ApplicationServices.Commands;
using Zamin.Core.Contracts.ApplicationServices.Events;
using Zamin.Core.Contracts.ApplicationServices.Queries;
using Zamin.Extensions.DependencyInjection;

namespace MiniBlog.Endpoints.WorkerService.Extensions;

public static class AddZaminDependenciesCustomizedExtentions
{
    public static IServiceCollection AddZaminDependenciesCustomized(this IServiceCollection services, params string[] assemblyNamesForSearch)
    {
        var assembliesForSearch = GetAssemblies(assemblyNamesForSearch);

        services.AddCommandHandlers(assembliesForSearch);
        services.AddCommandDispatcherDecorators();
        services.AddQueryHandlers(assembliesForSearch);
        services.AddEventHandlers(assembliesForSearch);
        services.AddValidatorsFromAssemblies(assembliesForSearch, ServiceLifetime.Transient);

        services.AddZaminDataAccess(assembliesForSearch);

        services.AddZaminUntilityServices();

        services.AddCustomeDepenecies(assembliesForSearch);

        return services;
    }

    private static IServiceCollection AddCommandHandlers(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        => services.AddWithTransientLifetime(assembliesForSearch, typeof(ICommandHandler<>), typeof(ICommandHandler<,>));

    private static IServiceCollection AddCommandDispatcherDecorators(this IServiceCollection services)
    {
        services.AddTransient<CommandDispatcher, CommandDispatcher>();
        services.AddTransient<CommandDispatcherDomainExceptionHandlerDecorator, CommandDispatcherDomainExceptionHandlerDecorator>();
        services.AddTransient<CommandDispatcherValidationDecorator, CommandDispatcherValidationDecorator>();
        services.AddTransient<ICommandDispatcher, CommandDispatcherValidationDecorator>();
        return services;
    }

    private static IServiceCollection AddQueryHandlers(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch) =>
        services.AddWithTransientLifetime(assembliesForSearch, typeof(IQueryHandler<,>), typeof(IQueryDispatcher));

    private static IServiceCollection AddEventHandlers(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch) =>
       services.AddWithTransientLifetime(assembliesForSearch, typeof(IDomainEventHandler<>), typeof(IEventDispatcher));

    private static List<Assembly> GetAssemblies(params string[] assemblyNamesForSearch)
    {
        var assemblies = new List<Assembly>();
        var dependencies = DependencyContext.Default.RuntimeLibraries;
        foreach (var library in dependencies)
        {
            if (IsCandidateCompilationLibrary(library, assemblyNamesForSearch))
            {
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                assemblies.Add(assembly);
            }
        }
        return assemblies;
    }

    private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary, string[] assmblyName)
    {
        return assmblyName.Any(d => compilationLibrary.Name.Contains(d))
            || compilationLibrary.Dependencies.Any(d => assmblyName.Any(c => d.Name.Contains(c)));
    }
}
