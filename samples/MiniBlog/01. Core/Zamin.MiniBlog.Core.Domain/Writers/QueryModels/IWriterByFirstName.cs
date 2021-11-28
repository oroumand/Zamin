using Zamin.Core.Contracts.ApplicationServices.Queries;

namespace Zamin.MiniBlog.Core.Domain.Writers.QueryModels
{
    public interface IWriterByFirstName : IPageQuery
    {
        public string FirstName { get; set; }
    }
}
