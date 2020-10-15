using Zamin.Utilities;
using System.Threading.Tasks;

namespace Zamin.Core.ApplicationServices.Queries
{

    public interface IQueryHandler<TQuery, TData>
            where TQuery : class, IQuery<TData>
    {
        Task<QueryResult<TData>> Handle(TQuery request);
    }

    public abstract class QueryHandler<TQuery, TData> : IQueryHandler<TQuery, TData>
        where TQuery : class, IQuery<TData>
    {
        protected readonly ZaminServices _zaminApplicationContext;
        public QueryHandler(ZaminServices eveApplicationContext)
        {
            _zaminApplicationContext = eveApplicationContext;
        }

        public Task<QueryResult<TData>> Handle(TQuery request)
        {
            throw new System.NotImplementedException();
        }
    }
}
