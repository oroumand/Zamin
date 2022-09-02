using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.Domain.Blogs.Entities;
using MiniBlog.Infra.Data.Sql.Commands.Common;
using Zamin.Infra.Data.Sql.Commands;

namespace MiniBlog.Infra.Data.Sql.Commands.Blogs
{
    public class BlogCommandRepository : 
        BaseCommandRepository<Blog, MiniblogCommandDbContext>, 
        IBlogCommandRepository
    {
        public BlogCommandRepository(MiniblogCommandDbContext dbContext) : base(dbContext)
        {
        }
    }
}
