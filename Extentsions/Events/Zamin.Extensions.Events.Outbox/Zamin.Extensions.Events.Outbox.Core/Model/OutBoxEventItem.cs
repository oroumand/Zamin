namespace Zamin.Extensions.Events.Outbox.Core.Model;
public class OutBoxEventItem
{
    public long OutBoxEventItemId { get; set; }
    public Guid EventId { get; set; }
    public string AccuredByUserId { get; set; }
    public DateTime AccuredOn { get; set; }
    public string AggregateName { get; set; }
    public string AggregateTypeName { get; set; }
    public string AggregateId { get; set; }
    public string EventName { get; set; }
    public string EventTypeName { get; set; }
    public string EventPayload { get; set; }
    public string? TraceId { get; set; }
    public string? SpanId { get; set; }
    public bool IsProcessed { get; set; }
}

