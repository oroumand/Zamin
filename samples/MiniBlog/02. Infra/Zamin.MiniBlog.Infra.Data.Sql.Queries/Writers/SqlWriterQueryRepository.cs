using Zamin.Core.Domain.Data;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.MiniBlog.Core.Domain.Writers.QueryModels;
using Zamin.MiniBlog.Core.Domain.Writers.Repositories;
using Zamin.MiniBlog.Infra.Data.Sql.Queries.Common;
using System.Collections.Generic;

namespace Zamin.MiniBlog.Infra.Data.Sql.Queries.Writers
{
    public class SqlWriterQueryRepository : BaseQueryRepository<MiniblogQueryDbContext>, IWriterQueryRepository
    {
        public SqlWriterQueryRepository(MiniblogQueryDbContext dbContext) : base(dbContext)
        {
        }

        public PagedData<List<WriterSummary>> Select(IWriterByFirstName writerByFirstName)
        {
            return new PagedData<List<WriterSummary>>();
        }
    }
}
