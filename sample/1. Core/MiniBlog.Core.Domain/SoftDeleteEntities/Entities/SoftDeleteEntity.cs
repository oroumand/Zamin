using Zamin.Core.Domain.Entities;

namespace MiniBlog.Core.Domain.SoftDeleteEntities.Entities
{
    public class SoftDeleteEntity : AggregateRoot
    {
        public string Name { get; private set; }

        private SoftDeleteEntity() { }

        public SoftDeleteEntity(string name)
        {
            Name = name;
        }
    }
}
