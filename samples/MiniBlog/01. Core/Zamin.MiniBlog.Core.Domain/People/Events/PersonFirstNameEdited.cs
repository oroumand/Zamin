using System;
using Zamin.Core.Domain.Events;

namespace Zamin.MiniBlog.Core.Domain.People.Events
{
    public record PersonFirstNameEdited(Guid BusinessId, string FirstName) : IDomainEvent;
}
