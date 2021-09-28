using System.Threading.Tasks;

namespace Zamin.Core.ApplicationServices.Commands
{
    public abstract class CommandDispatcherDecorator : ICommandDispatcher
    {
        #region Fields
        protected ICommandDispatcher _commandDispatcher; 
        #endregion

        #region Constructors
        public CommandDispatcherDecorator(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        } 
        #endregion
    
        #region Abstract Send Commands
        public abstract Task<CommandResult> Send<TCommand>(TCommand command) where TCommand : class, ICommand;

        public abstract Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command) where TCommand : class, ICommand<TData>; 
        #endregion
    }
}
