using System.Threading.Tasks;

namespace Zamin.Core.ApplicationServices.Commands
{
    public interface ICommandDispatcher
    {
        Task<CommandResult> Send<TCommand>(in TCommand command) where TCommand : class, ICommand;
        Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command) where TCommand : class, ICommand<TData>;

    }
}
