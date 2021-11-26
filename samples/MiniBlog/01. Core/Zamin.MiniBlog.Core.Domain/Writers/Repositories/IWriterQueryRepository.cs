using Zamin.MiniBlog.Core.Domain.Writers.QueryModels;

namespace Zamin.MiniBlog.Core.Domain.Writers.Repositories;

public interface IWriterQueryRepository : IQueryRepository
{
    PagedData<WriterSummary> Select(IWriterByFirstName writerByFirstName);
}