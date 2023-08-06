using Zamin.Core.Contracts.ApplicationServices.Queries;

namespace MiniBlog.Core.Contracts.Blogs.Queries.GetById;

public class GetBlogByIdQuery : IQuery<BlogQr?>
{
    public int BlogId { get; set; }
}