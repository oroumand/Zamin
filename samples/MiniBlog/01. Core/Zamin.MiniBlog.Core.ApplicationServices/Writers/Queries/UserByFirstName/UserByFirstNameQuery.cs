using Zamin.MiniBlog.Core.Domain.Writers.QueryModels;

namespace Zamin.MiniBlog.Core.ApplicationServices.Writers.Queries.UserByFirstName;

public class UserByFirstNameQuery : PageQuery<PagedData<WriterSummary>>, IWriterByFirstName
{
    public string FirstName { get; set; }
}