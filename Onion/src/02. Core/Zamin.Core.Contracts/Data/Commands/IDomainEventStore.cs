using Zamin.Core.Domain.Events;

namespace Zamin.Core.Contracts.Data.Commands;
/// <summary>
/// در صورت نیاز به ذخیره و بازیابی eventها از این اینترفیس استفاده می‌شود.
/// </summary>
public interface IDomainEventStore
{
    void Save<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events) where TEvent : IDomainEvent;
    Task SaveAsync<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events) where TEvent : IDomainEvent;
}

