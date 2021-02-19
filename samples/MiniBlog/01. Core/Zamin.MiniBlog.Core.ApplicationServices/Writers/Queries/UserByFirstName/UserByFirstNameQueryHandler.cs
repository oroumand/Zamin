using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.Domain.Data;
using Zamin.MiniBlog.Core.Domain.Writers.QueryModels;
using Zamin.MiniBlog.Core.Domain.Writers.Repositories;
using Zamin.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zamin.MiniBlog.Core.ApplicationServices.Writers.Queries.UserByFirstName
{
    public class UserByFirstNameQueryHandler : QueryHandler<UserByFirstNameQuery, PagedData<List<WriterSummary>>>
    {
        private readonly IWriterQueryRepository repository;

        public UserByFirstNameQueryHandler(ZaminServices zaminApplicationContext,IWriterQueryRepository repository) : base(zaminApplicationContext)
        {
            this.repository = repository;
        }

        public override Task<QueryResult<PagedData<List<WriterSummary>>>> Handle(UserByFirstNameQuery request)
        {
            var result = repository.Select(request);
            return ResultAsync(result);
            
        }
    }
}
