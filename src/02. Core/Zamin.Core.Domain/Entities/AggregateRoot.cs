﻿using Zamin.Core.Domain.Events;

namespace Zamin.Core.Domain.Entities;
/// <summary>
/// پیاده سازی الگوی AggregateRoot
/// توضیحات کامل در مورد این الگو را در آدرس زیر می‌توانید مشاهده نمایید
/// https://martinfowler.com/bliki/DDD_Aggregate.html
/// 
/// </summary>
public abstract class AggregateRoot : Entity
{
    /// <summary>
    /// لیست Evantهای مربوطه را نگهداری می‌کند        
    /// </summary>
    private readonly List<IDomainEvent> _events;
    protected AggregateRoot() => _events = new();

    /// <summary>
    /// سازنده Aggregate برای ایجاد Aggregate از روی Eventها
    /// </summary>
    /// <param name="events">در صورتی که Event از قبل وجود داشته باشد توسط این پارامتر به Aggregate ارسال می‌گردد</param>
    public AggregateRoot(IEnumerable<IDomainEvent> events)
    {
        if (events == null || !events.Any()) return;
        foreach (var @event in events)
            ((dynamic)this).On((dynamic)@event);
    }

    /// <summary>
    /// یک Event جدید به مجموعه Eventهای موجود در این Aggregate اضافه می‌کند.
    /// مسئولیت ساخت و ارسال Event به عهده خود Aggregateها می‌باشد.
    /// </summary>
    /// <param name="event"></param>
    protected void AddEvent(IDomainEvent @event) => _events.Add(@event);

    /// <summary>
    /// لیستی از Eventهای رخداده برای Aggregate را به صورت فقط خواندی و غیر قابل تغییر را بازگشت می‌دهد
    /// </summary>
    /// <returns>لیست Eventها</returns>
    public IEnumerable<IDomainEvent> GetEvents() => _events.AsEnumerable();

    /// <summary>
    /// Eventهای موجود در این Aggregate را پاک می‌کند
    /// </summary>
    public void ClearEvents() => _events.Clear();
}
