using System;
using Zamin.Core.Domain.Events;

namespace Zamin.MiniBlog.Core.Domain.Posts.Events
{
    public record PostRemoved(Guid BusinessId) : IDomainEvent;
}
