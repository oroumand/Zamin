using Zamin.Core.Contracts.ApplicationServices.Queries;

namespace Zamin.Core.ApplicationServices.Queries;

public abstract class QueryDispatcherDecorator : IQueryDispatcher
{
    #region Fields
    protected IQueryDispatcher _queryDispatcher;
    public abstract int Order { get; }
    #endregion

    #region Constructors
    public QueryDispatcherDecorator() { }
    #endregion

    #region Query Dispatcher
    public abstract Task<QueryResult<TData>> Execute<TQuery, TData>(TQuery query) where TQuery : class, IQuery<TData>;
    #endregion

    #region Methods
    public void SetQueryDispatcher(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }
    #endregion
}