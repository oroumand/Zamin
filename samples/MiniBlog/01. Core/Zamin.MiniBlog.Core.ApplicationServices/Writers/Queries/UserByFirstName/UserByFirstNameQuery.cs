using Zamin.Core.ApplicationServices.Queries;
using Zamin.MiniBlog.Core.Domain.Writers.QueryModels;
using System.Collections.Generic;
using Zamin.Core.Contracts.ApplicationServices.Queries;
using Zamin.Core.Contracts.Data.Queries;

namespace Zamin.MiniBlog.Core.ApplicationServices.Writers.Queries.UserByFirstName
{
    public class UserByFirstNameQuery : PageQuery<PagedData<List<WriterSummary>>>, IWriterByFirstName
    {
        public string FirstName { get; set; }
    }
}
