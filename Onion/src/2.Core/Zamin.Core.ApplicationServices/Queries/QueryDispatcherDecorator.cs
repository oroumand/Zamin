using Zamin.Core.Contracts.ApplicationServices.Queries;

namespace Zamin.Core.ApplicationServices.Queries;

public abstract class QueryDispatcherDecorator : IQueryDispatcher
{
    #region Fields
    protected IQueryDispatcher _queryDispatcher;
    #endregion

    #region Constructors
    public QueryDispatcherDecorator(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }
    #endregion

    #region Query Dispatcher
    public abstract Task<QueryResult<TData>> Execute<TQuery, TData>(TQuery query) where TQuery : class, IQuery<TData>;
    #endregion
}
