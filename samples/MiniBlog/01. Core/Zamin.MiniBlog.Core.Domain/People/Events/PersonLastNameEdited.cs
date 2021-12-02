using System;
using Zamin.Core.Domain.Events;

namespace Zamin.MiniBlog.Core.Domain.People.Events
{
    public record PersonLastNameEdited(Guid BusinessId, string LastName) : IDomainEvent;
}
