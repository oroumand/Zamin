using System;
using Zamin.Core.Domain.Events;

namespace Zamin.MiniBlog.Core.Domain.Posts.Events
{
    public record PostCreated(Guid BusinessId, string Title, string Content) : IDomainEvent;
}
