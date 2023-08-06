using Zamin.Core.Domain.Entities;

namespace MiniBlog.Core.Domain.People.Entities
{
    public class Person : AggregateRoot<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
