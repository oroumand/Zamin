using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Zamin.Infra.Data.Sql.Commands.Extensions;
using Zamin.Extentions.UsersManagement.Abstractions;
using Zamin.Extentions.Serializers.Abstractions;
using Zamin.Infra.Data.Sql.Commands.OutBoxEventItems;

namespace Zamin.Infra.Data.Sql.Commands.Interceptors;

public class AddOutBoxEventItemInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var changedAggregates = eventData.Context.ChangeTracker.GetAggregatesWithEvent();
        var userInfoService = eventData.Context.GetService<IUserInfoService>();
        var serializer = eventData.Context.GetService<IJsonSerializer>();
        foreach (var aggregate in changedAggregates)
        {
            var events = aggregate.GetEvents();
            foreach (var @event in events)
            {
                
                eventData.Context.Add(new OutBoxEventItem
                {
                    EventId = Guid.NewGuid(),
                    AccuredByUserId = userInfoService.UserId().ToString(),
                    AccuredOn = DateTime.Now,
                    AggregateId = aggregate.BusinessId.ToString(),
                    AggregateName = aggregate.GetType().Name,
                    AggregateTypeName = aggregate.GetType().FullName,
                    EventName = @event.GetType().Name,
                    EventTypeName = @event.GetType().FullName,
                    EventPayload = serializer.Serialize(@event),
                    IsProcessed = false
                });
            }
        }
        return base.SavingChanges(eventData, result);
    }
}