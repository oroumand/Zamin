using Zamin.Core.Domain.ValueObjects;
using System;

namespace Zamin.Core.Domain.Entities
{
    /// <summary>
    /// کلاس پایه برای تمامی Entityها موجود در سامانه
    /// </summary>
    public abstract class Entity : IAuditable
    {
        public long Id { get; protected set; }
        public BusinessId BusinessId { get; protected set; } = BusinessId.FromGuid(Guid.NewGuid());
        protected Entity() { }
    }
}
