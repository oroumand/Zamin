using Zamin.Core.Domain.Data;
using Zamin.MiniBlog.Core.Domain.Posts.Queries;
using Zamin.MiniBlog.Core.Domain.Posts.QueryResults;

namespace Zamin.MiniBlog.Core.Domain.Posts.Repositories
{
    public interface IPostQueryRepository : IQueryRepository
    {
        PostQr Select(IPostByBusinessIdQuery query);

        PostQr Select(IPostByTitleQuery query);

        PagedData<PostQr> Select(IPostsPagedQuery query);
    }
}
