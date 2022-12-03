using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Zamin.Infra.Data.Sql.Commands.Interceptors;

public class DeletedShadowPropertyInterceptor : SaveChangesInterceptor
{
    public static readonly string Deleted = nameof(Deleted);

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        SetDeletedPropertyValue(eventData);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        SetDeletedPropertyValue(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void SetDeletedPropertyValue(DbContextEventData eventData)
    {
        var changeTracker = eventData.Context.ChangeTracker;
        var deletedEntries = changeTracker.Entries().Where(x => x.State == EntityState.Deleted);

        foreach (var deletedEntry in deletedEntries)
        {
            if (deletedEntry.Properties.Any(c => c.Metadata.Name == Deleted))
            {
                deletedEntry.Property(Deleted).CurrentValue = true;
                deletedEntry.State = EntityState.Modified;
            }
        }
    }
}