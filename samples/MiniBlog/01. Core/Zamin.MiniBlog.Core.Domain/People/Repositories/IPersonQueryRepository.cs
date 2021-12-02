using Zamin.Core.Domain.Data;
using Zamin.MiniBlog.Core.Domain.People.Queries;
using Zamin.MiniBlog.Core.Domain.People.QueryResults;

namespace Zamin.MiniBlog.Core.Domain.People.Repositories
{
    public interface IPersonQueryRepository : IQueryRepository
    {
        PersonQr Select(IPersonByBusinessIdQuery query);

        PagedData<PersonQr> Select(IPeoplePagedQuery query);
    }
}
