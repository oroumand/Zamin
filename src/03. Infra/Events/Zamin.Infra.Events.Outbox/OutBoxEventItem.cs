using System;

namespace Zamin.Infra.Events.Outbox
{
    public class OutBoxEventItem
    {
        public long OutBoxEventItemId { get; set; }
        public string AccuredByUserId { get; set; }
        public string AggregateName { get; set; }
        public string AggregateTypeName { get; set; }
        public string AggregateId { get; set; }
        public string EventName { get; set; }
        public string EventTypeName { get; set; }
        public string EventPayload { get; set; }
        public DateTime EventDate { get; set; }
    }
}
