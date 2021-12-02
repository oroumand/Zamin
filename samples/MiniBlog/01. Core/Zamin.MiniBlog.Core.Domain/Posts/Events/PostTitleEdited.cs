using System;
using Zamin.Core.Domain.Events;

namespace Zamin.MiniBlog.Core.Domain.Posts.Events
{
    public record PostTitleEdited(Guid BusinessId, string Title) : IDomainEvent;
}
