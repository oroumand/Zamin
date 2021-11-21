using System;
using Zamin.Infra.Data.Sql.Queries;

namespace Zamin.MiniBlog.Infra.Data.Sql.Queries.Common.Models
{
    public class Writer : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int? CreatedByUserId { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? ModifiedByUserId { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}