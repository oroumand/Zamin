using System;

namespace Zamin.MiniBlog.Core.Domain.Posts.QueryResults
{
    public class PostQr
    {
        public long Id { get; set; }
        public Guid BusinessId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
