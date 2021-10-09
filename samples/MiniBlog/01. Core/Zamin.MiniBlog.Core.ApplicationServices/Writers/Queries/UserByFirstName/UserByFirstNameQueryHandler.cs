using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.Domain.Data;
using Zamin.MiniBlog.Core.Domain.Writers.QueryModels;
using Zamin.MiniBlog.Core.Domain.Writers.Repositories;
using Zamin.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zamin.MiniBlog.Core.ApplicationServices.Writers.Queries.UserByFirstName
{
    public class UserByFirstNameQueryHandler : QueryHandler<UserByFirstNameQuery, PagedData<WriterSummary>>
    {
        private readonly IWriterQueryRepository _writerQueryRepository;

        public UserByFirstNameQueryHandler(ZaminServices zaminApplicationContext, IWriterQueryRepository writerQueryRepository) : base(zaminApplicationContext)
        {
            _writerQueryRepository = writerQueryRepository;
        }
        public override Task<QueryResult<PagedData<WriterSummary>>> Handle(UserByFirstNameQuery request)
        {
            var result = _writerQueryRepository.Select(request);
            return ResultAsync(result);
        }
    }
}
