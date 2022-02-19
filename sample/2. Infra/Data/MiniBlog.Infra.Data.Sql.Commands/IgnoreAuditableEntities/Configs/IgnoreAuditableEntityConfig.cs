using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniBlog.Core.Domain.IgnoreAuditableEntities.Entities;

namespace MiniBlog.Infra.Data.Sql.Commands.IgnoreAuditableEntities.Configs
{
    public class IgnoreAuditableEntityConfig : IEntityTypeConfiguration<IgnoreAuditableEntity>
    {
        public void Configure(EntityTypeBuilder<IgnoreAuditableEntity> builder)
        {

        }
    }
}
