using System;
using System.Collections.Generic;
using System.Text;

namespace Zamin.Core.Domain.TacticalPatterns
{
    public abstract class Entity
    {
        public BusinessId Id { get; protected set; } = BusinessId.FromGuid(Guid.NewGuid());
        protected Entity() { }
    }
}
