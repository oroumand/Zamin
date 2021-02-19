using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.Domain.Data;
using Zamin.MiniBlog.Core.Domain.Writers.Entities;
using Zamin.MiniBlog.Core.Domain.Writers.QueryModels;
using System.Collections.Generic;

namespace Zamin.MiniBlog.Core.ApplicationServices.Writers.Queries.UserByFirstName
{
    public class UserByFirstNameQuery : PageQuery<PagedData<List<WriterSummary>>>, IWriterByFirstName
    {
        public string FirstName { get; set; }
    }
}
