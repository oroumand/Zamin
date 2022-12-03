using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zamin.Infra.Data.Sql.Commands.Extensions;

public static class QueryFilterExtensions
{
    public static EntityTypeBuilder<TEntity> IgnoreDeletedQueryFilter<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class
        => builder.HasQueryFilter(entity => EF.Property<bool>(entity, "Deleted") == false);
}