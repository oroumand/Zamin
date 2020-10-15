using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Zamin.Core.ApplicationServices.Queries
{

    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceScopeFactory _serviceFactory;
        public QueryDispatcher(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceFactory = serviceScopeFactory;
        }

        #region Query Dispatcher

        public Task<QueryResult<TData>> Execute<TQuery, TData>(TQuery query) where TQuery : class, IQuery<TData>
        {
            using var serviceScope = _serviceFactory.CreateScope();
            var handler = serviceScope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TData>>();
            return handler.Handle(query);
        }
        #endregion


    }
}
