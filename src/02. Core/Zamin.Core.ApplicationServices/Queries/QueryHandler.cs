﻿using Zamin.Utilities;
using Zamin.Core.Contracts.ApplicationServices.Common;
using Zamin.Core.Contracts.ApplicationServices.Queries;

namespace Zamin.Core.ApplicationServices.Queries;
public abstract class QueryHandler<TQuery, TData> : IQueryHandler<TQuery, TData>
    where TQuery : class, IQuery<TData>
{
    protected readonly ZaminServices _zaminApplicationContext;
    protected readonly QueryResult<TData> result = new QueryResult<TData>();

    protected virtual Task<QueryResult<TData>> ResultAsync(TData data, ApplicationServiceStatus status)
    {
        result._data = data;
        result.Status = status;
        return Task.FromResult(result);
    }

    protected virtual QueryResult<TData> Result(TData data, ApplicationServiceStatus status)
    {
        result._data = data;
        result.Status = status;
        return result;
    }


    protected virtual Task<QueryResult<TData>> ResultAsync(TData data)
    {
        var status = data != null ? ApplicationServiceStatus.Ok : ApplicationServiceStatus.NotFound;
        return ResultAsync(data, status);
    }

    protected virtual QueryResult<TData> Result(TData data)
    {
        var status = data != null ? ApplicationServiceStatus.Ok : ApplicationServiceStatus.NotFound;
        return Result(data, status);
    }

    public QueryHandler(ZaminServices zaminApplicationContext)
    {
        _zaminApplicationContext = zaminApplicationContext;
    }

    public abstract Task<QueryResult<TData>> Handle(TQuery request);
}
