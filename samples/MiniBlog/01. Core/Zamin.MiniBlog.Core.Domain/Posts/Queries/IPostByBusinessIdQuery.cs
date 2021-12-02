using System;

namespace Zamin.MiniBlog.Core.Domain.Posts.Queries
{
    public interface IPostByBusinessIdQuery
    {
        public Guid BusinessId { get; set; }
    }
}
