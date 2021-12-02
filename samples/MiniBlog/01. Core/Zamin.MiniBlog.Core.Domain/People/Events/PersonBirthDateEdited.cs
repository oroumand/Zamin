using System;
using Zamin.Core.Domain.Events;

namespace Zamin.MiniBlog.Core.Domain.People.Events
{
    public record PersonBirthDateEdited(Guid BusinessId, DateTime BirthDate) : IDomainEvent;
}
