using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zamin.Infra.Data.ChangeInterceptors.EntityChageInterceptorItems
{
    public class FakeEntityChageInterceptorItemRepository : IEntityChageInterceptorItemRepository
    {
        public FakeEntityChageInterceptorItemRepository()
        {

        }
        public void Save(List<EntityChageInterceptorItem> entityChageInterceptorItems)
        {
        }

        public Task SaveAsync(List<EntityChageInterceptorItem> entityChageInterceptorItems)
        {
            return Task.CompletedTask;
        }
    }
}
