using Zamin.MiniBlog.Core.Domain.Writers.QueryModels;
using System.Collections.Generic;
using Zamin.Core.Contracts.Data.Queries;

namespace Zamin.MiniBlog.Core.Domain.Writers.Repositories
{
    public interface IWriterQueryRepository : IQueryRepository
    {
        public PagedData<List<WriterSummary>> Select(IWriterByFirstName writerByFirstName);
    }
}
