using Zamin.Core.Domain.Events;

namespace MiniBlog.Core.Domain.Blogs.Events;

public record BlogUpdated(Guid BusinessId,
    string Title,
    string Description) : IDomainEvent;
