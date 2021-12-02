using System;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.MiniBlog.Core.Domain.People.Queries;
using Zamin.MiniBlog.Core.Domain.People.QueryResults;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Queries.PersonByBusinessId
{
    public class PersonByBusinessIdQuery : IQuery<PersonQr>, IPersonByBusinessIdQuery
    {
        public Guid BusinessId { get; set; }
    }
}
