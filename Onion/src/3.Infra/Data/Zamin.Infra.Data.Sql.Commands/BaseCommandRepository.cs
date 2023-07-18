using System.Linq.Expressions;

using Zamin.Core.Contracts.Data.Commands;
using Zamin.Core.Domain.Entities;
using Zamin.Core.Domain.ValueObjects;

namespace Zamin.Infra.Data.Sql.Commands;
public class BaseCommandRepository<TEntity, TDbContext, TId> : ICommandRepository<TEntity, TId>, IUnitOfWork
	where TEntity : AggregateRoot<TId>
	where TDbContext : BaseCommandDbContext
{
	protected readonly TDbContext _dbContext;

	public BaseCommandRepository(TDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	void ICommandRepository<TEntity, TId>.Delete(TId id)
	{
		var entity = _dbContext.Set<TEntity>().Find(id);
		_dbContext.Set<TEntity>().Remove(entity);
	}

	void ICommandRepository<TEntity, TId>.Delete(TEntity entity)
	{
		_dbContext.Set<TEntity>().Remove(entity);
	}

	void ICommandRepository<TEntity, TId>.DeleteGraph(TId id)
	{
		var graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
		IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
		foreach (var item in graphPath)
		{
			query = query.Include(item);
		}
		var entity = query.FirstOrDefault(c => c.Id.Equals(id));
		if (entity.Id != null) // todo check shavad agar id az noe long bood che ? bejay null 0 mishavad
			_dbContext.Set<TEntity>().Remove(entity);
	}

	TEntity ICommandRepository<TEntity, TId>.Get(TId id)
	{
		return _dbContext.Set<TEntity>().Find(id);
	}

	public TEntity Get(BusinessId businessId)
	{
		return _dbContext.Set<TEntity>().FirstOrDefault(c => c.BusinessId.Equals(businessId));
	}

	TEntity ICommandRepository<TEntity, TId>.GetGraph(TId id)
	{
		var graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
		IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
		foreach (var item in graphPath)
		{
			query = query.Include(item);
		}
		return query.FirstOrDefault(c => c.Id.Equals(id));
	}

	void ICommandRepository<TEntity, TId>.Insert(TEntity entity)
	{
		_dbContext.Set<TEntity>().Add(entity);
	}

	bool ICommandRepository<TEntity, TId>.Exists(Expression<Func<TEntity, bool>> expression)
	{
		return _dbContext.Set<TEntity>().Any(expression);
	}

	public TEntity GetGraph(BusinessId businessId)
	{
		var graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
		IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
		foreach (var item in graphPath)
		{
			query = query.Include(item);
		}
		return query.FirstOrDefault(c => c.BusinessId.Equals(businessId));
	}

	async Task ICommandRepository<TEntity, TId>.InsertAsync(TEntity entity)
	{
		await _dbContext.Set<TEntity>().AddAsync(entity);
	}

	async Task<TEntity> ICommandRepository<TEntity, TId>.GetAsync(TId id)
	{
		return await _dbContext.Set<TEntity>().FindAsync(id);
	}

	public Task<TEntity> GetAsync(BusinessId businessId)
	{
		return _dbContext.Set<TEntity>().FirstOrDefaultAsync(c => c.BusinessId.Equals(businessId));
	}

	Task<TEntity> ICommandRepository<TEntity, TId>.GetGraphAsync(TId id)
	{
		var graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
		IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
		foreach (var item in graphPath)
		{
			query = query.Include(item);
		}
		return query.FirstOrDefaultAsync(c => c.Id.Equals(id));
	}

	public Task<TEntity> GetGraphAsync(BusinessId businessId)
	{
		var graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
		IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
		foreach (var item in graphPath)
		{
			query = query.Include(item);
		}
		return query.FirstOrDefaultAsync(c => c.BusinessId.Equals(businessId));
	}

	Task<bool> ICommandRepository<TEntity, TId>.ExistsAsync(Expression<Func<TEntity, bool>> expression)
	{
		return _dbContext.Set<TEntity>().AnyAsync(expression);
	}

	public int Commit()
	{
		return _dbContext.SaveChanges();
	}

	public Task<int> CommitAsync()
	{
		return _dbContext.SaveChangesAsync();
	}

	public void BeginTransaction()
	{
		_dbContext.BeginTransaction();
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

