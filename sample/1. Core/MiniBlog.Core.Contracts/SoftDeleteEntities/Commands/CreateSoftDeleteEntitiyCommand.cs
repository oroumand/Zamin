using Zamin.Core.Contracts.ApplicationServices.Commands;

namespace MiniBlog.Core.Contracts.SoftDeleteEntities.Commands
{
    public class CreateSoftDeleteEntitiyCommand : ICommand<Guid>
    {
        public string Name { get; set; }
    }
}
