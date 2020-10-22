using Zamin.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zamin.MiniBlog.Core.Domain.Writers.Events
{
    public class PersonCreated:IDomainEvent
    {
        public string PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
