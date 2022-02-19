using MiniBlog.Core.Contracts.IgnoreAuditableEntities.Commands;
using MiniBlog.Core.Contracts.IgnoreAuditableEntities.Repositories;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Contracts.ApplicationServices.Commands;
using Zamin.Utilities;

namespace MiniBlog.Core.ApplicationService.IgnoreAuditableEntities.Commands.UpdateIgnoreAuditableEntitiy
{
    public class UpdateIgnoreAuditableEntitiyHandler : CommandHandler<UpdateIgnoreAuditableEntityCommand>
    {
        private readonly IIgnoreAuditableEntitiyCommandRepository _commandRepository;

        public UpdateIgnoreAuditableEntitiyHandler(
            ZaminServices zaminServices,
            IIgnoreAuditableEntitiyCommandRepository commandRepository) : base(zaminServices)
        {
            _commandRepository = commandRepository;
        }

        public override async Task<CommandResult> Handle(UpdateIgnoreAuditableEntityCommand command)
        {
            var ignoreAuditableEntitiy = await _commandRepository.GetAsync(command.IgnoreAuditableEntitiyBusinessId);

            ignoreAuditableEntitiy.Update(command.Name);

            await _commandRepository.CommitAsync();

            return await OkAsync();
        }
    }
}
