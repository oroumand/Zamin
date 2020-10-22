using Zamin.Infra.Data.Sql.Commands;
using Zamin.MiniBlog.Core.Domain;
using Zamin.Utilities;

namespace Zamin.MiniBlog.Infra.Data.Sql.Commands.Common
{
    public class MiniBlogUnitOfWork : BaseEntityFrameworkUnitOfWork<MiniblogDbContext>, IMiniblogUnitOfWork
    {
        public MiniBlogUnitOfWork(MiniblogDbContext dbContext, ZaminServices ZaminApplicationContext) : base(dbContext, ZaminApplicationContext)
        {
        }
    }
}
