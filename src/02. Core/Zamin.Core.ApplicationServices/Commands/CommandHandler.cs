using Zamin.Core.ApplicationServices.Common;
using Zamin.Utilities;
using System.Threading.Tasks;

namespace Zamin.Core.ApplicationServices.Commands
{
    public abstract class CommandHandler<TCommand, TData> : ICommandHandler<TCommand, TData>
    where TCommand : ICommand<TData>
    {
        protected readonly ZaminServices _ZaminServices;
        protected readonly CommandResult<TData> result = new CommandResult<TData>();

        public CommandHandler(ZaminServices ZaminServices)
        {
            _ZaminServices = ZaminServices;
        }
        public abstract Task<CommandResult<TData>> Handle(TCommand request);
        protected virtual Task<CommandResult<TData>> OkAsync(TData data)
        {
            result._data = data;
            result.Status = ApplicationServiceStatus.Ok;
            return Task.FromResult(result);
        }
        protected virtual CommandResult<TData> Ok(TData data)
        {
            result._data = data;
            result.Status = ApplicationServiceStatus.Ok;
            return result;
        }
        protected virtual Task<CommandResult<TData>> ResultAsync(TData data, ApplicationServiceStatus status)
        {
            result._data = data;
            result.Status = status;
            return Task.FromResult(result);
        }

        protected virtual CommandResult<TData> Result(TData data, ApplicationServiceStatus status)
        {
            result._data = data;
            result.Status = status;
            return result;
        }

        protected void AddMessage(string message)
        {
            result.AddMessage(_ZaminServices.ResourceManager[message]);
        }
        protected void AddMessage(string message, params string[] arguments)
        {
            result.AddMessage(_ZaminServices.ResourceManager[message, arguments]);
        }
    }

    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand
    {
        protected readonly ZaminServices _ZaminServices;
        protected readonly CommandResult result = new CommandResult();
        public CommandHandler(ZaminServices ZaminServices)
        {
            _ZaminServices = ZaminServices;
        }
        public abstract Task<CommandResult> Handle(TCommand request);

        protected virtual Task<CommandResult> OkAsync()
        {
            result.Status = ApplicationServiceStatus.Ok;
            return Task.FromResult(result);
        }

        protected virtual CommandResult Ok()
        {
            result.Status = ApplicationServiceStatus.Ok;
            return result;
        }

        protected virtual Task<CommandResult> ResultAsync(ApplicationServiceStatus status)
        {
            result.Status = status;
            return Task.FromResult(result);
        }
        protected virtual CommandResult Result(ApplicationServiceStatus status)
        {
            result.Status = status;
            return result;
        }
        protected void AddMessage(string message)
        {
            result.AddMessage(_ZaminServices.ResourceManager[message]);
        }
        protected void AddMessage(string message, params string[] arguments)
        {
            result.AddMessage(_ZaminServices.ResourceManager[message, arguments]);
        }
    }
}
