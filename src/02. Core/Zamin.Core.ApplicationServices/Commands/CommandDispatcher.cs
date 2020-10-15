using FluentValidation;
using Zamin.Core.ApplicationServices.Common;
using Zamin.Core.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Zamin.Toolkits;

namespace Zamin.Core.ApplicationServices.Commands
{

    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceScopeFactory _serviceFactory;
        private readonly ZaminServices _zaminServices;

        public CommandDispatcher(IServiceScopeFactory serviceScopeFactory, ZaminServices zaminServices)
        {
            _serviceFactory = serviceScopeFactory;
            _zaminServices = zaminServices;
        }

        public Task<CommandResult> Send<TCommand>(in TCommand command) where TCommand : class, ICommand
        {
            using var serviceScope = _serviceFactory.CreateScope();
            var handler = serviceScope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            var validationResult = Validate<TCommand, CommandResult>(command, serviceScope);
            if (validationResult != null)
            {
                return Task.FromResult(validationResult);
            }

            return DomainExceptionHandling<TCommand, CommandResult>(command, handler);

        }
        public Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command) where TCommand : class, ICommand<TData>
        {
            using var serviceScope = _serviceFactory.CreateScope();
            var handler = serviceScope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand, TData>>();
            var validationResult = Validate<TCommand, CommandResult<TData>>(command, serviceScope);

            if (validationResult != null)
            {
                return Task.FromResult(validationResult);
            }
            return DomainExceptionHandling<TCommand, CommandResult<TData>>(command, handler);
        }
        private Task<TCommandResult> DomainExceptionHandling<TCommand, TCommandResult>(TCommand command, dynamic commandHandler) where TCommandResult : ApplicationServiceResult, new()
        {
            try
            {
                return commandHandler.Handle(command);
            }
            catch (DomainStateException ex)
            {
                var type = typeof(TCommandResult);
                dynamic commandResult = new CommandResult();
                if (type.IsGenericType)
                {
                    var d1 = typeof(CommandResult<>);
                    var makeme = d1.MakeGenericType(type.GetGenericArguments());
                    commandResult = Activator.CreateInstance(makeme);
                }
                if (ex?.Parameters.Any() == true)
                {
                    commandResult.AddMessage(_zaminServices.ResourceManager[ex.Message, ex?.Parameters]);
                }
                else
                    commandResult.AddMessage(_zaminServices.ResourceManager[ex.Message]);

                commandResult.Status = ApplicationServiceStatus.InvalidDomainState;
                return Task.FromResult(commandResult as TCommandResult);
            }
        }
        private TValidationResult Validate<TCommand, TValidationResult>(TCommand command, IServiceScope serviceScope) where TValidationResult : ApplicationServiceResult, new()
        {
            var validator = serviceScope.ServiceProvider.GetService<IValidator<TCommand>>();
            if (validator != null)
            {
                var validationResult = validator.Validate(command);
                if (!validationResult.IsValid)
                {
                    TValidationResult res = new TValidationResult
                    {
                        Status = ApplicationServiceStatus.ValidationError
                    };
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
