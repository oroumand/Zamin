using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.Domain.Data;
using Zamin.MiniBlog.Core.Domain.People.Queries;
using Zamin.MiniBlog.Core.Domain.People.QueryResults;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Queries.PeoplePaged
{
    public class PeoplePagedQuery : PageQuery<PagedData<PersonQr>>, IPeoplePagedQuery
    {

    }
}
