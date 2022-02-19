using MiniBlog.Core.Contracts.SoftDeleteEntities.Commands;
using MiniBlog.Core.Contracts.SoftDeleteEntities.Repositories;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Contracts.ApplicationServices.Commands;
using Zamin.Utilities;

namespace MiniBlog.Core.ApplicationService.SoftDeleteEntities.Commands.DeleteSoftDeleteEntitiy
{
    public class DeleteSoftDeleteEntitiyHandler : CommandHandler<DeleteSoftDeleteEntitiyCommand>
    {
        private readonly ISoftDeleteEntitiyCommandRepository _commandRepository;

        public DeleteSoftDeleteEntitiyHandler(
            ZaminServices zaminServices,
            ISoftDeleteEntitiyCommandRepository commandRepository) : base(zaminServices)
        {
            _commandRepository = commandRepository;
        }

        public override async Task<CommandResult> Handle(DeleteSoftDeleteEntitiyCommand command)
        {
            var softDeleteEntitiy = await _commandRepository.GetAsync(command.SoftDeleteEntitiyBuseinessId);

            _commandRepository.Delete(softDeleteEntitiy);

            await _commandRepository.CommitAsync();

            return await OkAsync();
        }
    }
}
