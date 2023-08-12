

using MiniBlog.Core.RequestResponse.Blogs.Queries.GetById;

namespace MiniBlog.Core.Contracts.Blogs.Queries;

public interface IBlogQueryRepository
{
    public Task<BlogQr?> ExecuteAsync(GetBlogByIdQuery query);
}