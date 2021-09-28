using Zamin.Core.ApplicationServices.Common;
using System.Threading.Tasks;
using Zamin.Core.Domain.Exceptions;
using System;
using System.Linq;
using Zamin.Utilities.Services.Localizations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Zamin.Utilities.Services.Logger;

namespace Zamin.Core.ApplicationServices.Commands
{
    public class CommandDispatcherDomainExceptionHandlerDecorator : CommandDispatcherDecorator
    {
        #region Fields
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CommandDispatcherDomainExceptionHandlerDecorator> _logger;
        #endregion

        #region Constructors
        public CommandDispatcherDomainExceptionHandlerDecorator(CommandDispatcher commandDispatcher, IServiceProvider serviceProvider, ILogger<CommandDispatcherDomainExceptionHandlerDecorator> logger) : base(commandDispatcher)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        #endregion

        #region Send Commands
        public override Task<CommandResult> Send<TCommand>(TCommand command)
        {
            try
            {
                var result = _commandDispatcher.Send(command);
                var ex = result.Exception;
                return result;
            }
            catch (DomainStateException ex)
            {
                _logger.LogError(ZaminEventId.DomainValidationException, ex, "Processing of {CommandType} With value {Command} failed at {StartDateTime} because there are domain exceptions.", command.GetType(), command, DateTime.Now);
                return DomainExceptionHandlingWithoutReturnValue<TCommand>(ex);
            }

        }

        public override  Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command)
        {
            try
            {
                var result = _commandDispatcher.Send<TCommand, TData>(command);
                var ex = result.Exception;
                if(ex != null && ex is AggregateException)
                {
                    if(ex.InnerException != null && ex.InnerException is DomainStateException)
                    {
                        throw ex.InnerException;
                    }
                }
                return result;

            }
            catch (DomainStateException ex)
            {
                _logger.LogError(ZaminEventId.DomainValidationException, ex, "Processing of {CommandType} With value {Command} failed at {StartDateTime} because there are domain exceptions.", command.GetType(), command, DateTime.Now);
                return DomainExceptionHandlingWithReturnValue<TCommand, TData>(ex);
            }
        }
        #endregion

        #region Privaite Methods
        private Task<CommandResult> DomainExceptionHandlingWithoutReturnValue<TCommand>(DomainStateException ex)
        {
            var commandResult = new CommandResult
            {
                Status = ApplicationServiceStatus.InvalidDomainState
            };

            commandResult.AddMessage(GetExceptionText(ex));

            return Task.FromResult(commandResult);
        }

        private Task<CommandResult<TData>> DomainExceptionHandlingWithReturnValue<TCommand, TData>(DomainStateException ex)
        {
            var commandResult = new CommandResult<TData>()
            {
                Status = ApplicationServiceStatus.InvalidDomainState
            };

            commandResult.AddMessage(GetExceptionText(ex));

            return Task.FromResult(commandResult);
        }

        private string GetExceptionText(DomainStateException domainStateException)
        {
            var translator = _serviceProvider.GetService<ITranslator>();
            if (translator == null)
                return domainStateException.ToString();

            var result = (domainStateException?.Parameters.Any() == true) ?
                 translator[domainStateException.Message, domainStateException?.Parameters] :
                   translator[domainStateException.Message];

            _logger.LogInformation(ZaminEventId.DomainValidationException, "Domain Exception message is {DomainExceptionMessage}", result);

            return result;
        }
        #endregion
    }
}
