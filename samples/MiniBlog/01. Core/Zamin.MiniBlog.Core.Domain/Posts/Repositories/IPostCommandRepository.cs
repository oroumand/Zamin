using System.Collections.Generic;
using System.Threading.Tasks;
using Zamin.Core.Domain.Data;
using Zamin.Core.Domain.ValueObjects;
using Zamin.MiniBlog.Core.Domain.Posts.Entities;

namespace Zamin.MiniBlog.Core.Domain.Posts.Repositories
{
    public interface IPostCommandRepository : ICommandRepository<Post>
    {
        Task<IEnumerable<Post>> GetByPersonBusinessIdAsync(BusinessId personBusinessIdId);
    }
}
