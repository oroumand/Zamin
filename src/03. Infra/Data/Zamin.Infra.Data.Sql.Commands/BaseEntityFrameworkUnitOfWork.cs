using Zamin.Utilities;
using System.Threading.Tasks;

namespace Zamin.Infra.Data.Sql.Commands
{
    public abstract class BaseEntityFrameworkUnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : BaseCommandDbContext
    {
        protected readonly TDbContext _dbContext;
        protected readonly ZaminServices _zaminApplicationService;

        public BaseEntityFrameworkUnitOfWork(TDbContext dbContext, ZaminServices zaminApplicationContext)
        {
            _dbContext = dbContext;
            _zaminApplicationService = zaminApplicationContext;
        }

        public void BeginTransaction()
        {
            _dbContext.BeginTransaction();
        }

        public int Commit()
        {
            var result = _dbContext.SaveChanges();
            return result;
        }

        public async Task<int> CommitAsync()
        {
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public void CommitTransaction()
        {
            _dbContext.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _dbContext.RollbackTransaction();
        }
    }
}
