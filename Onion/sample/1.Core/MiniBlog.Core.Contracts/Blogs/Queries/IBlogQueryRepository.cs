using MiniBlog.Core.Contracts.Blogs.Queries.GetBlogByBusinessId;

namespace MiniBlog.Core.Contracts.Blogs.Queries;

public interface IBlogQueryRepository
{
    public Task<BlogQr> Execute(GetBlogByBusinessIdQuery query);
}