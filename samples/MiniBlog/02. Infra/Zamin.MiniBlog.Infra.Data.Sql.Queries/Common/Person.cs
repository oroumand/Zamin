using System;
using System.Collections.Generic;

namespace Zamin.MiniBlog.Infra.Data.Sql.Queries.Common
{
    public partial class Person
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? ModifiedByUserId { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public Guid BusinessId { get; set; }
    }
}
