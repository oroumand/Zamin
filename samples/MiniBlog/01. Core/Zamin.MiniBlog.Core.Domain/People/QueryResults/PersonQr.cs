using System;

namespace Zamin.MiniBlog.Core.Domain.People.QueryResults
{
    public class PersonQr
    {
        public long Id { get; set; }
        public Guid BusinessId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
