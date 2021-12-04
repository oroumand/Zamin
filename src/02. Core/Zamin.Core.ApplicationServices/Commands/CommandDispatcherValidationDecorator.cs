﻿using FluentValidation;
using Zamin.Utilities.Services.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Zamin.Core.Contracts.ApplicationServices.Commands;
using Zamin.Core.Contracts.ApplicationServices.Common;

namespace Zamin.Core.ApplicationServices.Commands;
public class CommandDispatcherValidationDecorator : CommandDispatcherDecorator
{
    #region Fields
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<CommandDispatcherValidationDecorator> _logger;
    #endregion

    #region Constructors
    public CommandDispatcherValidationDecorator(CommandDispatcherDomainExceptionHandlerDecorator commandDispatcher,
                                                IServiceProvider serviceProvider, ILogger<CommandDispatcherValidationDecorator> logger)
                                                : base(commandDispatcher)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    #endregion

    #region Send Commands
    public override Task<CommandResult> Send<TCommand>(TCommand command)
    {
        _logger.LogDebug(ZaminEventId.CommandValidation, "Validating command of type {CommandType} With value {Command}  start at :{StartDateTime}", command.GetType(), command, DateTime.Now);
        var validationResult = Validate<TCommand, CommandResult>(command);

        if (validationResult != null)
        {
            _logger.LogInformation(ZaminEventId.CommandValidation, "Validating command of type {CommandType} With value {Command}  failed. Validation errors are: {ValidationErrors}", command.GetType(), command, validationResult.Messages);
            return Task.FromResult(validationResult);
        }
        _logger.LogDebug(ZaminEventId.CommandValidation, "Validating command of type {CommandType} With value {Command}  finished at :{EndDateTime}", command.GetType(), command, DateTime.Now);
        return _commandDispatcher.Send(command);
    }

    public override Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command)
    {
        _logger.LogDebug(ZaminEventId.CommandValidation, "Validating command of type {CommandType} With value {Command}  start at :{StartDateTime}", command.GetType(), command, DateTime.Now);

        var validationResult = Validate<TCommand, CommandResult<TData>>(command);

        if (validationResult != null)
        {
            _logger.LogInformation(ZaminEventId.CommandValidation, "Validating command of type {CommandType} With value {Command}  failed. Validation errors are: {ValidationErrors}", command.GetType(), command, validationResult.Messages);
            return Task.FromResult(validationResult);
        }
        _logger.LogDebug(ZaminEventId.CommandValidation, "Validating command of type {CommandType} With value {Command}  finished at :{EndDateTime}", command.GetType(), command, DateTime.Now);
        return _commandDispatcher.Send<TCommand, TData>(command);
    }
    #endregion

    #region Privaite Methods
    private TValidationResult Validate<TCommand, TValidationResult>(TCommand command) where TValidationResult : ApplicationServiceResult, new()
    {
        var validator = _serviceProvider.GetService<IValidator<TCommand>>();
        TValidationResult res = null;

        if (validator != null)
        {
            var validationResult = validator.Validate(command);
            if (!validationResult.IsValid)
            {
                res = new()
                {
                    Status = ApplicationServiceStatus.ValidationError
                };
                foreach (var item in validationResult.Errors)
                {
                    res.AddMessage(item.ErrorMessage);
                }
            }
        }
        else
        {
            _logger.LogInformation(ZaminEventId.CommandValidation, "There is not any validator for {CommandType}", command.GetType());
        }
        return res;
    }
    #endregion
}
