using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zamin.Core.Domain.ValueObjects;
using Zamin.Infra.Data.Sql.Commands;
using Zamin.MiniBlog.Core.Domain.Posts.Entities;
using Zamin.MiniBlog.Core.Domain.Posts.Repositories;
using Zamin.MiniBlog.Infra.Data.Sql.Commands.Common;

namespace Zamin.MiniBlog.Infra.Data.Sql.Commands.Posts
{
    public class PostCommandRepository : BaseCommandRepository<Post, MiniblogCommandDbContext>, IPostCommandRepository
    {
        public PostCommandRepository(MiniblogCommandDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Post>> GetByPersonBusinessIdAsync(BusinessId personBusinessIdId)
            => await _dbContext.Posts.Where(c => c.PersonBusinessId == BusinessId.FromGuid(personBusinessIdId.Value)).ToListAsync();
    }
}
