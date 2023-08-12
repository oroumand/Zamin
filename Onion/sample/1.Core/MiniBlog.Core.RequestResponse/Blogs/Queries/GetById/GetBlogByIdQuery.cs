using Zamin.Core.RequestResponse.Queries;

namespace MiniBlog.Core.RequestResponse.Blogs.Queries.GetById;

public class GetBlogByIdQuery : IQuery<BlogQr?>
{
    public int BlogId { get; set; }
}