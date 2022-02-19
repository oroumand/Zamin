using MiniBlog.Core.Domain.SoftDeleteEntities.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace MiniBlog.Core.Contracts.SoftDeleteEntities.Repositories
{
    public interface ISoftDeleteEntitiyCommandRepository : ICommandRepository<SoftDeleteEntity>
    {
    }
}
