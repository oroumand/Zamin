using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.Contracts.ApplicationServices.Queries;

namespace MiniBlog.Endpoints.API.CustomDecorators;

public class CustomQueryDecorator : QueryDispatcherDecorator
{
    public override int Order => 0;

    public override async Task<QueryResult<TData>> Execute<TQuery, TData>(TQuery query)
    {
        return await _queryDispatcher.Execute<TQuery, TData>(query);
    }
}