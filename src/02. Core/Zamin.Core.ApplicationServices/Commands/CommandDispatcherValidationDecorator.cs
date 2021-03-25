using FluentValidation;
using Zamin.Core.ApplicationServices.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Zamin.Core.ApplicationServices.Commands
{
    public class CommandDispatcherValidationDecorator : CommandDispatcherDecorator
    {
        private readonly IServiceProvider _serviceProvider;
        public CommandDispatcherValidationDecorator(CommandDispatcherDomainExceptionHandlerDecorator commandDispatcher, IServiceProvider serviceProvider) : base(commandDispatcher)
        {
            _serviceProvider = serviceProvider;
        }
        public override Task<CommandResult> Send<TCommand>(in TCommand command)
        {
            var validationResult = Validate<TCommand, CommandResult>(command);
            if (validationResult != null)
            {
                return Task.FromResult(validationResult);
            }
            return _commandDispatcher.Send(command);
        }

        public override Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command)
        {
            var validationResult = Validate<TCommand, CommandResult<TData>>(command);
            if (validationResult != null)
            {
                return Task.FromResult(validationResult);
            }
            return _commandDispatcher.Send<TCommand, TData>(command);
        }

        private TValidationResult Validate<TCommand, TValidationResult>(TCommand command) where TValidationResult : ApplicationServiceResult, new()
        {
            var validator = _serviceProvider.GetService<IValidator<TCommand>>();
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
