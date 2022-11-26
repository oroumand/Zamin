using System.Xml.Linq;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Contracts.ApplicationServices.Commands;

namespace MiniBlog.Endpoints.API.CommandDecorator
{
    public class CustomCommandDecorator : CommandDispatcherDecorator
    {
        public override int Order => 0;

        public override async Task<CommandResult> Send<TCommand>(TCommand command)
        {
            int a = 1;
            return await _commandDispatcher.Send<TCommand>(command);
        }

        public override async Task<CommandResult<TData>> Send<TCommand, TData>(TCommand command)
        {
            int a = 1;
            return await _commandDispatcher.Send<TCommand,TData>(command);
        }
    }
}
