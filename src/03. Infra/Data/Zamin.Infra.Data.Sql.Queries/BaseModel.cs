using System;

namespace Zamin.Infra.Data.Sql.Queries
{
    public class BaseModel
    {
        public long Id { get; set; }
        public Guid BusinessId { get; set; }
    }
}
