using Zamin.Core.Domain.Entities;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.Domain.ValueObjects;
using Zamin.MiniBlog.Core.Domain.Writers.Events;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Zamin.MiniBlog.Core.Domain.Writers.Entities
{
    public class Writer:AggregateRoot
    {
        public Title FirstName{ get; private set; }
        public Title LastName { get; private set; }
        public Writer(BusinessId businessId, Title firstName,Title lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            BusinessId = businessId;
            AddEvent(new PersonCreated
            {
                FirstName = FirstName.Value,
                LastName = LastName.Value,
                PersonId = BusinessId.Value.ToString()
            });
        }
    }
}
