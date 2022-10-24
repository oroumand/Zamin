using Zamin.Infra.Events.Outbox;
using Zamin.Utilities.Configurations;
using Zamin.Utilities.Services.MessageBus;
using Microsoft.Extensions.Hosting;

namespace Zamin.Infra.Events.PoolingPublisher;
public class PoolingPublisherHostedService : IHostedService
{
    private readonly ZaminConfigurationOptions _configuration;
    private readonly IOutBoxEventItemRepository _outBoxEventItemRepository;
    private readonly ISendMessageBus _messageBus;
    static readonly object _locker = new();
    public PoolingPublisherHostedService(ZaminConfigurationOptions configuration, IOutBoxEventItemRepository outBoxEventItemRepository, ISendMessageBus messageBus)
    {
        _configuration = configuration;
        _outBoxEventItemRepository = outBoxEventItemRepository;
        _messageBus = messageBus;

    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Timer _timer = new(SendOutBoxItems, null, TimeSpan.Zero, TimeSpan.FromSeconds(_configuration.PoolingPublisher.SendOutBoxInterval));
        return Task.CompletedTask;
    }

    private void SendOutBoxItems(object? state)
    {
        lock (_locker)
        {
            var outboxItems = _outBoxEventItemRepository.GetOutBoxEventItemsForPublishe(_configuration.PoolingPublisher.SendOutBoxCount);

            foreach (var item in outboxItems)
            {
                _messageBus.Send(new Parcel
                {
                    CorrelationId = item.AggregateId,
                    MessageBody = item.EventPayload,
                    MessageId = item.EventId.ToString(),
                    MessageName = item.EventName,
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
            }
            _outBoxEventItemRepository.MarkAsRead(outboxItems);
        }

    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

}