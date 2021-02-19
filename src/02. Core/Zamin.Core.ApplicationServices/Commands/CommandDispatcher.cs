using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Zamin.Core.ApplicationServices.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceScopeFactory _serviceFactory;

        public CommandDispatcher(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceFactory = serviceScopeFactory;
        }

        public Task<CommandResult> Send<TCommand>(in TCommand command) where TCommand : class, ICommand
        {
            using var serviceScope = _serviceFactory.CreateScope();
            var handler = serviceScope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            return handler.Handle(command);

        }

        public Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command) where TCommand : class, ICommand<TData>
        {
            using var serviceScope = _serviceFactory.CreateScope();
            var handler = serviceScope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand, TData>>();
            return handler.Handle(command);
        }
    }
}
