using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Zamin.Infra.Data.Sql.Commands.Extensions;
using Zamin.Extentions.UsersManagement.Abstractions;
using Zamin.Extentions.Serializers.Abstractions;
using Zamin.Infra.Data.Sql.Commands.OutBoxEventItems;
using System.Diagnostics;

namespace Zamin.Infra.Data.Sql.Commands.Interceptors;

public class AddOutBoxEventItemInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        AddOutbox(eventData);
        return base.SavingChanges(eventData, result);
    }

    private static void AddOutbox(DbContextEventData eventData)
    {
        var changedAggregates = eventData.Context.ChangeTracker.GetAggregatesWithEvent();
        var userInfoService = eventData.Context.GetService<IUserInfoService>();
        var serializer = eventData.Context.GetService<IJsonSerializer>();
        string traceId = string.Empty;
        string spanId = string.Empty;
        if(Activity.Current != null)
        {
            traceId = Activity.Current.TraceId.ToHexString();
            spanId= Activity.Current.SpanId.ToHexString();
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
                    AggregateTypeName = aggregate.GetType().FullName,
                    EventName = @event.GetType().Name,
                    EventTypeName = @event.GetType().FullName,
                    EventPayload = serializer.Serialize(@event),
                    TraceId= traceId,
                    SpanId= spanId,
                    IsProcessed = false
                });
            }
        }
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        AddOutbox(eventData);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
