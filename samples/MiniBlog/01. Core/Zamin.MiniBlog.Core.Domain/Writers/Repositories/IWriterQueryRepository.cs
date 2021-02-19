using Zamin.Core.Domain.Data;
using Zamin.MiniBlog.Core.Domain.Writers.QueryModels;
using System.Collections.Generic;

namespace Zamin.MiniBlog.Core.Domain.Writers.Repositories
{
    public interface IWriterQueryRepository:IQueryRepository
    {
        public PagedData<List<WriterSummary>> Select(IWriterByFirstName writerByFirstName);
    }
}
