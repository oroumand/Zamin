using System;
using System.Threading.Tasks;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.Domain.Data;
using Zamin.MiniBlog.Core.Domain.People.QueryResults;
using Zamin.MiniBlog.Core.Domain.People.Repositories;
using Zamin.Utilities;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Queries.PeoplePaged
{
    public class PeoplePagedQueryHandler : QueryHandler<PeoplePagedQuery, PagedData<PersonQr>>
    {
        private readonly IPersonQueryRepository _queryRepository;

        public PeoplePagedQueryHandler(
            ZaminServices zaminApplicationContext,
            IPersonQueryRepository queryRepository) : base(zaminApplicationContext)
        {
            _queryRepository = queryRepository;
        }

        public override async Task<QueryResult<PagedData<PersonQr>>> Handle(PeoplePagedQuery peoplePaged)
            => await ResultAsync(_queryRepository.Select(peoplePaged));
    }
}
