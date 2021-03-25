using Zamin.Core.Domain.Entities;

namespace Zamin.MiniBlog.Core.Domain.People.Entities
{
    public class Person : AggregateRoot
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
    }
}
