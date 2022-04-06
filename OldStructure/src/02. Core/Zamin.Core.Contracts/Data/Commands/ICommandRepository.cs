using System.Linq.Expressions;
using Zamin.Core.Domain.Entities;
using Zamin.Core.Domain.ValueObjects;

namespace Zamin.Core.Contracts.Data.Commands;
/// <summary>
/// در صورتی که داده‌ها به صورت عادی ذخیره سازی شوند از این Interface جهت تعیین اعمال اصلی موجود در بخش ذخیره سازی داده‌ها استفاده می‌شود.
/// </summary>
/// <typeparam name="TEntity">کلاسی که جهت ذخیره سازی انتخاب می‌شود</typeparam>
public interface ICommandRepository<TEntity> : IUnitOfWork
    where TEntity : AggregateRoot
{
    /// <summary>
    /// یک شی را با شناسه حذف می کند
    /// </summary>
    /// <param name="id">شناسه</param>
    void Delete(long id);

    /// <summary>
    /// حذف یک شی به همراه تمامی فرزندان آن را انجام می دهد
    /// </summary>
    /// <param name="id">شناسه</param>
    void DeleteGraph(long id);

    /// <summary>
    /// یک شی را دریافت کرده و از دیتابیس حذف می‌کند
    /// </summary>
    /// <param name="entity"></param>
    void Delete(TEntity entity);

    /// <summary>
    /// داده‌های جدید را به دیتابیس اضافه می‌کند
    /// </summary>
    /// <param name="entity">نمونه داده‌ای که باید به دیتابیس اضافه شود.</param>
    void Insert(TEntity entity);

    /// <summary>
    /// داده‌های جدید را به دیتابیس اضافه می‌کند
    /// </summary>
    /// <param name="entity">نمونه داده‌ای که باید به دیتابیس اضافه شود.</param>
    Task InsertAsync(TEntity entity);

    /// <summary>
    /// یک شی را با شناسه از دیتابیس یافته و بازگشت می‌دهد.
    /// </summary>
    /// <param name="id">شناسه شی مورد نیاز</param>
    /// <returns>نمونه ساخته شده از شی</returns>
    TEntity Get(long id);
    Task<TEntity> GetAsync(long id);
    TEntity Get(BusinessId businessId);
    Task<TEntity> GetAsync(BusinessId businessId);
    TEntity GetGraph(long id);
    Task<TEntity> GetGraphAsync(long id);
    TEntity GetGraph(BusinessId businessId);
    Task<TEntity> GetGraphAsync(BusinessId businessId);
    bool Exists(Expression<Func<TEntity, bool>> expression);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
}
