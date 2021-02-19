using Zamin.Core.Domain.Events;

namespace Zamin.MiniBlog.Core.Domain.Writers.Events
{
    public class PersonCreated:IDomainEvent
    {
        public string PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class PersonUpdated : IDomainEvent
    {
        public string PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
