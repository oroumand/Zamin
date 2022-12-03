using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zamin.Infra.Data.Sql.Extentions;

public static class DeletedShadowProperty
{
    public static readonly string Deleted = nameof(Deleted);

    public static EntityTypeBuilder<TEntity> AddDeletedShadowProperty<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class
    {
        builder.Property<bool>(Deleted).HasDefaultValue(false);

        return builder;
    }
}