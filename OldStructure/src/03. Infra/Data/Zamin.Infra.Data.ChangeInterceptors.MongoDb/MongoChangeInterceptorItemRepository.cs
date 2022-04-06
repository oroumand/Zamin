using System.Collections.Generic;
using System.Threading.Tasks;
using Zamin.Infra.Data.ChangeInterceptors.EntityChageInterceptorItems;

namespace Zamin.Infra.Data.ChangeInterceptors.MongoDb
{
    public class MongoEntityChangeInterceptorItemRepository : IEntityChageInterceptorItemRepository
    {
        public void Save(List<EntityChageInterceptorItem> entityChageInterceptorItems)
        {
            
        }

        public Task SaveAsync(List<EntityChageInterceptorItem> entityChageInterceptorItems)
        {
            throw new System.NotImplementedException();
        }
    }
}