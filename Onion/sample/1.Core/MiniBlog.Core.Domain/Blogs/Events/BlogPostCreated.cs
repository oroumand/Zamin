using Zamin.Core.Domain.Events;

namespace MiniBlog.Core.Domain.Blogs.Events;

public record BlogPostCreated(Guid BusinessId,
    Guid BlogBusinessId,
    string Title) : IDomainEvent;
