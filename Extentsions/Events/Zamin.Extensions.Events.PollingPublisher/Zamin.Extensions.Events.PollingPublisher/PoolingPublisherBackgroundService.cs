using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Zamin.Extensions.Events.Abstractions;
using Zamin.Extensions.Events.PollingPublisher.Options;
using Zamin.Extentions.MessageBus.Abstractions;

namespace Zamin.Extensions.Events.PollingPublisher
{
    public class PoolingPublisherBackgroundService : BackgroundService
    {
        private readonly ISendMessageBus _sendMessageBus;
        private readonly IOutBoxEventItemRepository _outBoxEventItemRepository;
        private readonly ILogger<PoolingPublisherBackgroundService> _logger;
        private readonly PollingPublisherOptions _options;

        public PoolingPublisherBackgroundService( IOutBoxEventItemRepository outBoxEventItemRepository, IOptions<PollingPublisherOptions> options, ILogger<PoolingPublisherBackgroundService> logger,IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();

            _options = options.Value;
            _sendMessageBus = scope.ServiceProvider.GetRequiredService<ISendMessageBus>();
            _outBoxEventItemRepository = outBoxEventItemRepository;
            _logger = logger;
            _logger.LogInformation("PoolingPublisherBackgroundService start working for {ApplicationName} at {DateTime}", _options.ApplicationName, DateTime.Now);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var outboxItems = _outBoxEventItemRepository.GetOutBoxEventItemsForPublishe(_options.SendCount);
                    foreach (var item in outboxItems)
                    {
                        using Activity trace = StartChildActivity(item);
                        _sendMessageBus.Send(new Parcel
                        {
                            CorrelationId = item.AggregateId,
                            MessageBody = item.EventPayload,
                            MessageId = item.EventId.ToString(),
                            MessageName = item.EventName,
                            Route = $"{_options.ApplicationName}.event.{item.EventName}",
                            Headers = new Dictionary<string, object>
                            {
                                ["AccuredByUserId"] = item.AccuredByUserId,
                                ["AccuredOn"] = item.AccuredOn.ToString(),
                                ["AggregateName"] = item.AggregateName,
                                ["AggregateTypeName"] = item.AggregateTypeName,
                                ["EventTypeName"] = item.EventTypeName,
                            }
                        });
                        item.IsProcessed = true;
                        _logger.LogDebug("event {eventName} with {EventId} sent from {ApplicaotinName} at {DateTime}", item.EventName, item.EventId, _options.ApplicationName, DateTime.Now);
                    }
                    _outBoxEventItemRepository.MarkAsRead(outboxItems);
                    if (outboxItems.Any())
                    {
                        _logger.LogInformation("{Count} events {ApplicaotinName} at {DateTime} with id {Ids}", outboxItems.Count, _options.ApplicationName, DateTime.Now, string.Join(',', outboxItems.Select(c => c.EventId)));
                    }
                    await Task.Delay(_options.SendInterval, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Exception acquired in PoolingPublisherBackgroundService for application {ApplicaitonName}", _options.ApplicationName);
                    await Task.Delay(_options.ExceptionInterval, stoppingToken);

                }

            }
            _logger.LogInformation("PoolingPublisherBackgroundService stop working for {ApplicationName} at {DateTime}", _options.ApplicationName, DateTime.Now);
        }

        private static Activity StartChildActivity(OutBoxEventItem item)
        {
            var trace = new Activity("PublishUsingPoolingPublisher");
            if (item.TraceId != null && item.SpanId != null)
            {
                trace.SetParentId($"00-{item.TraceId}-{item.SpanId}-00");
            }
            trace.Start();
            return trace;
        }
    }
}