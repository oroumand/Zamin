using Zamin.Core.Domain.Data;
using Zamin.MiniBlog.Core.Domain.Writers.Entities;

namespace Zamin.MiniBlog.Core.Domain.Writers.Repositories
{
    public interface IWriterRepository : ICommandRepository<Writer>
    {
    }
}
