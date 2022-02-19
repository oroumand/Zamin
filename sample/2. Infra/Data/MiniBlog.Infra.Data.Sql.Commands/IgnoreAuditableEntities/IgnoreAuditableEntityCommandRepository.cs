using MiniBlog.Core.Contracts.IgnoreAuditableEntities.Repositories;
using MiniBlog.Core.Domain.IgnoreAuditableEntities.Entities;
using MiniBlog.Infra.Data.Sql.Commands.Common;
using Zamin.Infra.Data.Sql.Commands;

namespace MiniBlog.Infra.Data.Sql.Commands.IgnoreAuditableEntities
{
    public class IgnoreAuditableEntityCommandRepository
        : BaseCommandRepository<IgnoreAuditableEntity, MiniblogCommandDbContext>, IIgnoreAuditableEntitiyCommandRepository
    {
        public IgnoreAuditableEntityCommandRepository(MiniblogCommandDbContext dbContext) : base(dbContext)
        {
        }
    }
}
