using Zamin.Core.Domain.Data;

namespace Zamin.MiniBlog.Core.Domain.Writers.QueryModels
{
    public interface IWriterByFirstName : IPageQuery
    {
        public string FirstName { get; set; }
    }
}
