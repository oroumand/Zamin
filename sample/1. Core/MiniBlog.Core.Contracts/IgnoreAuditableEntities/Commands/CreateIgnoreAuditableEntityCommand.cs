using Zamin.Core.Contracts.ApplicationServices.Commands;

namespace MiniBlog.Core.Contracts.IgnoreAuditableEntities.Commands
{
    public class CreateIgnoreAuditableEntityCommand : ICommand<Guid>
    {
        public string Name { get; set; }
    }
}
