using System;

namespace Zamin.Infra.Data.Sql.Queries
{
    public class BaseModel
    {
        public long Id { get; set; }
        public Guid BusinessId { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? ModifiedByUserId { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}