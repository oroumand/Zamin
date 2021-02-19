using System.Threading.Tasks;

namespace Zamin.Core.ApplicationServices.Commands
{
    public abstract class CommandDispatcherChain : ICommandDispatcher
    {
        protected ICommandDispatcher _commandDispatcher;
        public CommandDispatcherChain(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }
        public abstract Task<CommandResult> Send<TCommand>(in TCommand command) where TCommand : class, ICommand;

        public abstract Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command) where TCommand : class, ICommand<TData>;
    }

}
