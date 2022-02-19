using Zamin.Core.Contracts.ApplicationServices.Commands;

namespace MiniBlog.Core.Contracts.IgnoreAuditableEntities.Commands
{
    public class UpdateIgnoreAuditableEntityCommand : ICommand
    {
        public Guid IgnoreAuditableEntitiyBusinessId { get; set; }
        public string Name { get; set; }
    }
}
