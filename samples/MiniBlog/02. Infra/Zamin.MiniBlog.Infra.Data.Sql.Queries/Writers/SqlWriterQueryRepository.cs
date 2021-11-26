using Zamin.MiniBlog.Core.Domain.Writers.QueryModels;
using Zamin.MiniBlog.Core.Domain.Writers.Repositories;
using Zamin.MiniBlog.Infra.Data.Sql.Queries.Common;
using Zamin.MiniBlog.Utilities;

namespace Zamin.MiniBlog.Infra.Data.Sql.Queries.Writers;

public class SqlWriterQueryRepository : BaseQueryRepository<MiniblogQueryDbContext>, IWriterQueryRepository
{
    public SqlWriterQueryRepository(MiniblogQueryDbContext dbContext) : base(dbContext)
    {
    }

    public PagedData<WriterSummary> Select(IWriterByFirstName writerByFirstName)
    {
        #region Query

        var query = _dbContext.Writers.AsQueryable();

        #endregion

        #region Filter

        query = query.WhereIf(!string.IsNullOrEmpty(writerByFirstName.FirstName), w => w.FirstName == writerByFirstName.FirstName);

        #endregion

        #region Result

        var result = query.Select(w => new WriterSummary()
        {
            FirstName = w.FirstName,
            LastName = w.LastName
        }).ToPageData(writerByFirstName);

        #endregion

        return result;
    }
}
