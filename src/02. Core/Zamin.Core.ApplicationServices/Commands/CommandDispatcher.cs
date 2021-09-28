using Zamin.Utilities.Services.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Zamin.Core.ApplicationServices.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        #region Fields
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CommandDispatcher> _logger;
        private readonly Stopwatch _stopwatch;
        #endregion

        #region Constructors
        public CommandDispatcher(IServiceProvider serviceProvider, ILogger<CommandDispatcher> logger)
        {
            _serviceProvider = serviceProvider;
            _stopwatch = new Stopwatch();
            _logger = logger;
        }
        #endregion

        #region Send Commands
        public Task<CommandResult> Send<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            _stopwatch.Start();
            try
            {
                _logger.LogDebug("Routing command of type {CommandType} With value {Command}  Start at {StartDateTime}", command.GetType(), command, DateTime.Now);
                var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
                return handler.Handle(command);

            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "There is not suitable handler for {CommandType} Routing failed at {StartDateTime}.", command.GetType(), DateTime.Now);
                throw;
            }
            finally
            {
                _stopwatch.Stop();
                _logger.LogInformation(ZaminEventId.PerformanceMeasurement,"Processing the {CommandType} command tooks {Millisecconds} Millisecconds", command.GetType(),_stopwatch.ElapsedMilliseconds);
            }

        }

        public Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command) where TCommand : class, ICommand<TData>
        {
            _stopwatch.Start();
            try
            {
                _logger.LogDebug("Routing command of type {CommandType} With value {Command}  Start at {StartDateTime}", command.GetType(), command, DateTime.Now);
                var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TData>>();
                var result = handler.Handle(command);

                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not suitable handler for {CommandType} Routing failed at {StartDateTime}.", command.GetType(), DateTime.Now);
                throw;
            }
            finally
            {
                _stopwatch.Stop();
                _logger.LogInformation("Processing the {CommandType} command tooks {Millisecconds} Millisecconds", command.GetType(), _stopwatch.ElapsedMilliseconds);
            }
        }
        #endregion

    }
}
