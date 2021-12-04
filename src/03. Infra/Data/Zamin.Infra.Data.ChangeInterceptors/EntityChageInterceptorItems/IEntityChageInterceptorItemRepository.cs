namespace Zamin.Infra.Data.ChangeInterceptors.EntityChageInterceptorItems;
public interface IEntityChageInterceptorItemRepository
{
    public void Save(List<EntityChageInterceptorItem> entityChageInterceptorItems);
    public Task SaveAsync(List<EntityChageInterceptorItem> entityChageInterceptorItems);
}
