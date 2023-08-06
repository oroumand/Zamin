using Zamin.Core.Domain.Events;

namespace MiniBlog.Core.Domain.Blogs.Events;

public record BlogPostDeleted(Guid BusinessId,
    Guid BlogBusinessId) : IDomainEvent;
