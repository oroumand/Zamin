using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics;
using Zamin.Core.Domain.Entities;
using Zamin.Extensions.Events.Abstractions;
using Zamin.Extensions.Serializers.Abstractions;
using Zamin.Extensions.UsersManagement.Abstractions;

namespace Zamin.Extensions.Events.Outbox.Dal.EF.Interceptors;

public class AddOutBoxEventItemInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        AddOutbox(eventData);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        AddOutbox(eventData);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void AddOutbox(DbContextEventData eventData)
    {
        List<dynamic> changedAggregates = eventData.Context.ChangeTracker
            .Entries<IAggregateRoot>()
            .Where(x => x.State != EntityState.Detached)
            .Select(c => c.Entity as dynamic)
            .Where(c => c.GetEvents() != null && c.GetEvents().Count > 0)
            .ToList();

        if (changedAggregates is null || !changedAggregates.Any())
        {
            return;
        }

        var userInfoService = eventData.Context.GetService<IUserInfoService>();
        var serializer = eventData.Context.GetService<IJsonSerializer>();
        string traceId = string.Empty;
        string spanId = string.Empty;

        if (Activity.Current != null)
        {
            traceId = Activity.Current.TraceId.ToHexString();
            spanId = Activity.Current.SpanId.ToHexString();
        }

        foreach (var aggregate in changedAggregates)
        {
            var events = aggregate.GetEvents();
            foreach (object @event in events)
            {
                eventData.Context.Add(new OutBoxEventItem
                {
                    EventId = Guid.NewGuid(),
                    AccuredByUserId = userInfoService.UserIdOrDefault(),
                    AccuredOn = DateTime.Now,
                    AggregateId = aggregate.BusinessId.ToString(),
                    AggregateName = aggregate.GetType().Name,
                    AggregateTypeName = aggregate.GetType().FullName ?? aggregate.GetType().Name,
                    EventName = @event.GetType().Name,
                    EventTypeName = @event.GetType().FullName ?? @event.GetType().Name,
                    EventPayload = serializer.Serialize(@event),
                    TraceId = traceId,
                    SpanId = spanId,
                    IsProcessed = false
                });
            }
        }
    }

}
