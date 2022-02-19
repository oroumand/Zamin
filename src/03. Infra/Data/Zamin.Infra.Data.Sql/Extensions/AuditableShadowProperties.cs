using Zamin.Core.Domain.Entities;
using Zamin.Utilities.Services.Users;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Zamin.Infra.Data.Sql.Extensions;
public static class AuditableShadowProperties
{
    public static readonly Func<object, int?> EFPropertyCreatedByUserId = entity => EF.Property<int?>(entity, CreatedByUserId);
    public static readonly string CreatedByUserId = nameof(CreatedByUserId);

    public static readonly Func<object, int?> EFPropertyModifiedByUserId = entity => EF.Property<int?>(entity, ModifiedByUserId);
    public static readonly string ModifiedByUserId = nameof(ModifiedByUserId);

    public static readonly Func<object, DateTime?> EFPropertyCreatedDateTime = entity => EF.Property<DateTime?>(entity, CreatedDateTime);
    public static readonly string CreatedDateTime = nameof(CreatedDateTime);

    public static readonly Func<object, DateTime?> EFPropertyModifiedDateTime = entity => EF.Property<DateTime?>(entity, ModifiedDateTime);
    public static readonly string ModifiedDateTime = nameof(ModifiedDateTime);

    public static void AddAuditableShadowProperties(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(
            e => typeof(IAuditable).IsAssignableFrom(e.ClrType) && !typeof(IIgnoreAuditable).IsAssignableFrom(e.ClrType)))
        {
            modelBuilder.Entity(entityType.ClrType).Property<int?>(CreatedByUserId);
            modelBuilder.Entity(entityType.ClrType).Property<int?>(ModifiedByUserId);
            modelBuilder.Entity(entityType.ClrType).Property<DateTime?>(CreatedDateTime);
            modelBuilder.Entity(entityType.ClrType).Property<DateTime?>(ModifiedDateTime);
        }
    }

    public static void SetAuditableEntityPropertyValues(this ChangeTracker changeTracker, IUserInfoService userInfoService)
    {
        var userAgent = userInfoService.GetUserAgent();
        var userIp = userInfoService.GetUserIp();
        var now = DateTime.UtcNow;
        var userId = userInfoService.UserId();

        var modifiedEntries = changeTracker.Entries<IAuditable>().Where(
            x => x.State == EntityState.Modified && x.Entity is not IIgnoreAuditable);
        foreach (var modifiedEntry in modifiedEntries)
        {
            modifiedEntry.Property(ModifiedDateTime).CurrentValue = now;
            modifiedEntry.Property(ModifiedByUserId).CurrentValue = userId;
        }

        var addedEntries = changeTracker.Entries<IAuditable>().Where(
            x => x.State == EntityState.Added && x.Entity is not IIgnoreAuditable);
        foreach (var addedEntry in addedEntries)
        {
            addedEntry.Property(CreatedDateTime).CurrentValue = now;
            addedEntry.Property(CreatedByUserId).CurrentValue = userId;
        }
    }

    public static List<EntityEntry<IAuditable>> GetChangedAuditable(this ChangeTracker changeTracker)
        => changeTracker.Entries<IAuditable>().Where(
            x => x.State == EntityState.Modified || x.State == EntityState.Added || x.State == EntityState.Deleted).ToList();

}

