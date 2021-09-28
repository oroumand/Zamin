using Zamin.Core.Domain.Entities;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.Domain.ValueObjects;

namespace Zamin.MiniBlog.Core.Domain.Writers.Entities
{
    public class Writer : AggregateRoot
    {
        public Title FirstName { get; private set; }
        public Title LastName { get; private set; }
        public Writer(BusinessId businessId, Title firstName, Title lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            BusinessId = businessId;
        }

        public void Update(Title firstName, Title lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
