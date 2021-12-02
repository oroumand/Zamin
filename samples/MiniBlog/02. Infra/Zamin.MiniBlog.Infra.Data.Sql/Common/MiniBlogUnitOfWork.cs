using Zamin.Infra.Data.Sql.Commands;
using Zamin.MiniBlog.Core.Domain;
using Zamin.Utilities;

namespace Zamin.MiniBlog.Infra.Data.Sql.Commands.Common
{
    public class MiniBlogUnitOfWork : BaseEntityFrameworkUnitOfWork<MiniblogCommandDbContext>, IMiniblogUnitOfWork
    {
        public MiniBlogUnitOfWork(MiniblogCommandDbContext dbContext, ZaminServices zaminApplicationContext) : base(dbContext, zaminApplicationContext)
        {
        }
    }
}
