using FluentValidation;
using Zamin.Core.ApplicationServices.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Zamin.Core.ApplicationServices.Commands
{
    public class CommandDispatcherValidationChain : CommandDispatcherChain
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CommandDispatcherValidationChain(CommandDispatcherDomainExceptionHandlerChain commandDispatcher, IServiceScopeFactory serviceScopeFactory) : base(commandDispatcher)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override Task<CommandResult> Send<TCommand>(in TCommand command)
        {
            using var serviceScope = _serviceScopeFactory.CreateScope();
            var validationResult = Validate<TCommand, CommandResult>(command, serviceScope);
            if (validationResult != null)
            {
                return Task.FromResult(validationResult);
            }
            return _commandDispatcher.Send(command);
        }

        public override Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command)
        {
            using var serviceScope = _serviceScopeFactory.CreateScope();
            var validationResult = Validate<TCommand, CommandResult<TData>>(command, serviceScope);
            if (validationResult != null)
            {
                return Task.FromResult(validationResult);
            }
            return _commandDispatcher.Send<TCommand,TData>(command);
        }

        private static TValidationResult Validate<TCommand, TValidationResult>(TCommand command, IServiceScope serviceScope) where TValidationResult : ApplicationServiceResult, new()
        {
            var validator = serviceScope.ServiceProvider.GetService<IValidator<TCommand>>();
            if (validator != null)
            {
                var validationResult = validator.Validate(command);
                if (!validationResult.IsValid)
                {
                    TValidationResult res = new TValidationResult();
                    res.Status = ApplicationServiceStatus.ValidationError;
                    foreach (var item in validationResult.Errors)
                    {
                        res.AddMessage(item.ErrorMessage);
                    }

                    return res;
                }
            }
            return null;
        }
    }
}
