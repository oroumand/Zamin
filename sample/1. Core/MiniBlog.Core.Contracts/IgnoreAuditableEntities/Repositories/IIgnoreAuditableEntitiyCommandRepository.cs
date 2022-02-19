using MiniBlog.Core.Domain.IgnoreAuditableEntities.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace MiniBlog.Core.Contracts.IgnoreAuditableEntities.Repositories
{
    public interface IIgnoreAuditableEntitiyCommandRepository : ICommandRepository<IgnoreAuditableEntity>
    {
    }
}
