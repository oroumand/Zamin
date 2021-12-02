using System.Threading.Tasks;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.MiniBlog.Core.Domain.People.QueryResults;
using Zamin.MiniBlog.Core.Domain.People.Repositories;
using Zamin.Utilities;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Queries.PersonByBusinessId
{
    public class PersonByBusinessIdQueryHandler : QueryHandler<PersonByBusinessIdQuery, PersonQr>
    {
        private readonly IPersonQueryRepository _queryRepository;

        public PersonByBusinessIdQueryHandler(
            ZaminServices zaminApplicationContext,
            IPersonQueryRepository queryRepository) : base(zaminApplicationContext)
        {
            _queryRepository = queryRepository;
        }

        public override async Task<QueryResult<PersonQr>> Handle(PersonByBusinessIdQuery personByBusinessId)
            => await ResultAsync(_queryRepository.Select(personByBusinessId));
    }
}
