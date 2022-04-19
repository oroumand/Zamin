using Zamin.Core.Contracts.Data.Commands;

namespace Zamin.Infra.Data.Sql.Commands;
public abstract class BaseEntityFrameworkUnitOfWork<TDbContext> : IUnitOfWork
    where TDbContext : BaseCommandDbContext
{
    protected readonly TDbContext _dbContext;

    public BaseEntityFrameworkUnitOfWork(TDbContext dbContext)
    {
        _dbContext = dbContext;
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

