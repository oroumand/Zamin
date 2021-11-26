﻿using Zamin.MiniBlog.Core.Domain.Writers.QueryModels;
using Zamin.MiniBlog.Core.Domain.Writers.Repositories;

namespace Zamin.MiniBlog.Core.ApplicationServices.Writers.Queries.UserByFirstName;

public class UserByFirstNameQueryHandler : QueryHandler<UserByFirstNameQuery, PagedData<WriterSummary>>
{
    private readonly IWriterQueryRepository repository;

    public UserByFirstNameQueryHandler(ZaminServices zaminApplicationContext, IWriterQueryRepository repository) : base(zaminApplicationContext)
    {
        this.repository = repository;
    }

    public override Task<QueryResult<PagedData<WriterSummary>>> Handle(UserByFirstNameQuery request)
    {
        var result = repository.Select(request);
        return ResultAsync(result);

    }
}