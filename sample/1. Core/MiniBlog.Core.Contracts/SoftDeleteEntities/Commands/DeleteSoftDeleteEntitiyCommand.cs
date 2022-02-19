using Zamin.Core.Contracts.ApplicationServices.Commands;

namespace MiniBlog.Core.Contracts.SoftDeleteEntities.Commands
{
    public class DeleteSoftDeleteEntitiyCommand : ICommand
    {
        public Guid SoftDeleteEntitiyBuseinessId { get; set; }
    }
}
