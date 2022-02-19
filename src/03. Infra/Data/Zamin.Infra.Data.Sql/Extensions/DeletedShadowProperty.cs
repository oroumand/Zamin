using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zamin.Infra.Data.Sql.Configurations
{
    public static class DeletedShadowProperty
    {
        public static readonly string Deleted = nameof(Deleted);
        public static readonly string Test = nameof(Test);

        public static EntityTypeBuilder<TEntity> AddDeletedShadowProperty<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class
        {
            builder.Property<bool>(Deleted).HasDefaultValue(false);

            return builder;
        }

        public static void SetDeletedPropertyValue(this ChangeTracker changeTracker)
        {
            var deletedEntries = changeTracker.Entries().Where(x => x.State == EntityState.Deleted);

            foreach (var deletedEntry in deletedEntries)
            {
                if (deletedEntry.Properties.Any(c => c.Metadata.Name == "Deleted"))
                {
                    deletedEntry.Property(Deleted).CurrentValue = true;
                    deletedEntry.State = EntityState.Modified;
                }
            }
        }
    }
}