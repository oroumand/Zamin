using System;
using System.Collections.Generic;

namespace Zamin.MiniBlog.Infra.Data.Sql.Queries.Common
{
    public partial class Post
    {
        public long Id { get; set; }
        public Guid PersonBusinessId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? ModifiedByUserId { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public Guid BusinessId { get; set; }
    }
}
