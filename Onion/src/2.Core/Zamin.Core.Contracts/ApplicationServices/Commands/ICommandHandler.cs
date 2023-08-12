using Zamin.Core.RequestResponse.Commands;

namespace Zamin.Core.Contracts.ApplicationServices.Commands;

public interface ICommandHandler<TCommand, TData> where TCommand : ICommand<TData>
{
    Task<CommandResult<TData>> Handle(TCommand request);
}
public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task<CommandResult> Handle(TCommand request);
}

