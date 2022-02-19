using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniBlog.Core.Domain.SoftDeleteEntities.Entities;
using Zamin.Infra.Data.Sql.Configurations;

namespace MiniBlog.Infra.Data.Sql.Commands.SoftDeleteEntities.Config
{
    public class SoftDeleteEntitiyConfig : IEntityTypeConfiguration<SoftDeleteEntity>
    {
        public void Configure(EntityTypeBuilder<SoftDeleteEntity> builder)
        {
            builder.AddDeletedShadowProperty().IgnoreDeletedQueryFilter();
        }
    }
}
