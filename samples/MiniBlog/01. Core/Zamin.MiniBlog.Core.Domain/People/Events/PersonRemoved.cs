using System;
using Zamin.Core.Domain.Events;

namespace Zamin.MiniBlog.Core.Domain.People.Events
{
    public record PersonRemoved(Guid BusinessId) : IDomainEvent;
}
