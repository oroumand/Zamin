using System;
using System.Linq.Expressions;
using Zamin.Core.Domain.Entities;
using Zamin.Core.Domain.ValueObjects;

namespace Zamin.Core.Domain.Data
{
    public interface ICommandRepository<TEntity> : IUnitOfWork
        where TEntity : AggregateRoot
    {
        void Delete(long id);
        void DeleteGraph(long id);
        void Delete(TEntity entity);
        void Insert(TEntity entity);
        TEntity Get(long id);
        TEntity Get(BusinessId businessId);
        TEntity GetGraph(long id);
        TEntity GetGraph(BusinessId businessId);
        bool Exists(Expression<Func<TEntity, bool>> expression);
        
        Task InsertAsync(TEntity entity);
        Task<TEntity> GetAsync(long id);
        Task<TEntity> GetAsync(BusinessId businessId);
        Task<TEntity> GetGraphAsync(long id);
        Task<TEntity> GetGraphAsync(BusinessId businessId);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
    }
}
