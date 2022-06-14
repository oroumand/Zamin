using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Zamin.Infra.Data.Sql.Commands.Extensions;
using Zamin.Extentions.UsersManagement.Abstractions;

namespace Zamin.Infra.Data.Sql.Commands.Interceptors;

public class AddAuditDataInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var changeTracker = eventData.Context.ChangeTracker;
        var userInfoService = eventData.Context.GetService<IUserInfoService>();
        changeTracker.SetAuditableEntityPropertyValues(userInfoService);
        return base.SavingChanges(eventData, result);
    }
}
