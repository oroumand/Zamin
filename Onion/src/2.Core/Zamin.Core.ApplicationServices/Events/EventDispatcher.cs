using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Zamin.Core.Contracts.ApplicationServices.Events;
using Zamin.Core.Domain.Events;

namespace Zamin.Core.ApplicationServices.Events;

public class EventDispatcher : IEventDispatcher
{
    #region Fields
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<EventDispatcher> _logger;
    private readonly Stopwatch _stopwatch;
    #endregion

    #region Constructors
    public EventDispatcher(IServiceProvider serviceProvider, ILogger<EventDispatcher> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _stopwatch = new Stopwatch();
    }
    #endregion

    #region Event Dispatcher
    public async Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : class, IDomainEvent
    {
        _stopwatch.Start();
        int counter = 0;
        try
        {
            _logger.LogDebug("Routing event of type {EventType} With value {Event}  Start at {StartDateTime}", @event.GetType(), @event, DateTime.Now);
            var handlers = _serviceProvider.GetServices<IDomainEventHandler<TDomainEvent>>();
            List<Task> tasks = new List<Task>();
            foreach (var handler in handlers)
            {
                counter++;
                tasks.Add(handler.Handle(@event));
            }
            await Task.WhenAll(tasks);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "There is not suitable handler for {EventType} Routing failed at {StartDateTime}.", @event.GetType(), DateTime.Now);
            throw;
        }
        finally
        {
            _stopwatch.Stop();
            _logger.LogDebug("Total number of handler for {EventType} is {Count} ,EventHandlers tooks {Millisecconds} Millisecconds", @event.GetType(), counter, _stopwatch.ElapsedMilliseconds);
        }
    }
    #endregion
}