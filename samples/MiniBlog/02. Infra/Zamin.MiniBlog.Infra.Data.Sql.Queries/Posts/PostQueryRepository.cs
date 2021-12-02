using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Zamin.Core.Domain.Data;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.MiniBlog.Core.Domain.Posts.Queries;
using Zamin.MiniBlog.Core.Domain.Posts.QueryResults;
using Zamin.MiniBlog.Core.Domain.Posts.Repositories;
using Zamin.MiniBlog.Infra.Data.Sql.Queries.Common;

namespace Zamin.MiniBlog.Infra.Data.Sql.Queries.Posts
{
    public class PostQueryRepository : BaseQueryRepository<MiniblogQueryDbContext>, IPostQueryRepository
    {
        public PostQueryRepository(MiniblogQueryDbContext dbContext) : base(dbContext)
        {
        }

        public PostQr Select(IPostByBusinessIdQuery query)
            => PostDb.FirstOrDefault(c => c.BusinessId == query.BusinessId);

        public PostQr Select(IPostByTitleQuery query)
            => PostDb.FirstOrDefault(c => c.Title.Contains(query.Title));

        public PagedData<PostQr> Select(IPostsPagedQuery query)
        {
            throw new NotImplementedException();
        }

        private IQueryable<PostQr> PostDb
            => _dbContext.Posts.AsNoTracking()
            .Select(c => new PostQr()
            {
                Id = c.Id,
                BusinessId = c.BusinessId,
                Title = c.Title,
                Content = c.Content,
            });
    }
}
