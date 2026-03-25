using System.Data;

namespace Zamin.Extensions.ChangeDataLog.Abstractions;
public interface IEntityChageInterceptorItemRepository
{
    public void Save(List<EntityChageInterceptorItem> entityChageInterceptorItems, IDbTransaction transaction);
    public Task SaveAsync(List<EntityChageInterceptorItem> entityChageInterceptorItems, IDbTransaction transaction);
}
