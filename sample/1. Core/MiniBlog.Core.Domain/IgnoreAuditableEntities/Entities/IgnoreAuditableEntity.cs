using Zamin.Core.Domain.Entities;

namespace MiniBlog.Core.Domain.IgnoreAuditableEntities.Entities
{
    public class IgnoreAuditableEntity : AggregateRoot, IIgnoreAuditable
    {
        public string Name { get; private set; }

        private IgnoreAuditableEntity() { }

        public IgnoreAuditableEntity(string name)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }
    }
}
