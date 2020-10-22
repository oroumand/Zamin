using Zamin.Utilities.Services.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zamin.Infra.Events.Outbox
{
    public class OutboxEventPublisher : IHostedService
    {
        private readonly IOutBoxEventItemRepository _outBoxEventItemRepository;
        private readonly IAsyncMessagePublisher _asyncMessagePublisher;
        private Timer _timer;
        private int _sendOutBoxInterval;
        private int _sendOutBoxCount;
        public OutboxEventPublisher(IConfiguration configuration,IOutBoxEventItemRepository outBoxEventItemRepository, IAsyncMessagePublisher asyncMessagePublisher)
        {
            _outBoxEventItemRepository = outBoxEventItemRepository;
            _asyncMessagePublisher = asyncMessagePublisher;
            _sendOutBoxInterval = int.Parse(configuration["ZaminConfigurations:Messaging:EventOutbox:SendOutBoxInterval"]);
            _sendOutBoxCount = int.Parse(configuration["ZaminConfigurations:Messaging:EventOutbox:SendOutBoxCount"]);
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SendOutBoxItems, null, TimeSpan.Zero, TimeSpan.FromSeconds(_sendOutBoxInterval));
            return Task.CompletedTask;
        }

        private void SendOutBoxItems(object state)
        {
            _timer.Change(Timeout.Infinite, 0);
            var outboxItems = _outBoxEventItemRepository.GetOutBoxEventItemsForPublishe(_sendOutBoxCount);

            foreach (var item in outboxItems)
            {
                _asyncMessagePublisher.Publish(item.EventId.ToString(), item.AggregateId, item.EventName, item.EventPayload);
                item.IsProcessed = true;
            }
            _outBoxEventItemRepository.MarkAsRead(outboxItems);
            _timer.Change(0, _sendOutBoxInterval);

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
