using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Zamin.Core.ApplicationServices.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<CommandResult> Send<TCommand>(in TCommand command) where TCommand : class, ICommand
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            return handler.Handle(command);

        }

        public Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command) where TCommand : class, ICommand<TData>
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TData>>();
            return handler.Handle(command);
        }

    }
}
