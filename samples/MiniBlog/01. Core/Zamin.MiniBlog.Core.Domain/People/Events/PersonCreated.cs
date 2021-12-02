using System;
using Zamin.Core.Domain.Events;

namespace Zamin.MiniBlog.Core.Domain.People.Events
{
    public record PersonCreated(Guid BusinessId, string FirstName, string LastName, DateTime? BirthDate) : IDomainEvent;
}
