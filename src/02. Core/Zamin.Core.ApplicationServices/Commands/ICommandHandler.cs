using System.Threading.Tasks;

namespace Zamin.Core.ApplicationServices.Commands
{
    public interface ICommandHandler<TCommand, TData> where TCommand : ICommand<TData>
    {
        Task<CommandResult<TData>> Handle(TCommand request);
    }
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task<CommandResult> Handle(TCommand request);
    }
}
