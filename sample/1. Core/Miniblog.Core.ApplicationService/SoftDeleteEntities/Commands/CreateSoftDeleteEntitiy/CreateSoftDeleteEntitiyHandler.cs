using MiniBlog.Core.Contracts.SoftDeleteEntities.Commands;
using MiniBlog.Core.Contracts.SoftDeleteEntities.Repositories;
using MiniBlog.Core.Domain.SoftDeleteEntities.Entities;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Contracts.ApplicationServices.Commands;
using Zamin.Utilities;

namespace MiniBlog.Core.ApplicationService.SoftDeleteEntities.Commands.CreateSoftDeleteEntitiy
{
    public class CreateSoftDeleteEntitiyHandler : CommandHandler<CreateSoftDeleteEntitiyCommand, Guid>
    {
        private readonly ISoftDeleteEntitiyCommandRepository _commandRepository;

        public CreateSoftDeleteEntitiyHandler(
            ZaminServices zaminServices,
            ISoftDeleteEntitiyCommandRepository commandRepository) : base(zaminServices)
        {
            _commandRepository = commandRepository;
        }

        public override async Task<CommandResult<Guid>> Handle(CreateSoftDeleteEntitiyCommand command)
        {
            var softDeleteEntitiy = new SoftDeleteEntity(command.Name);

            await _commandRepository.InsertAsync(softDeleteEntitiy);

            await _commandRepository.CommitAsync();

            return await OkAsync(softDeleteEntitiy.BusinessId.Value);
        }
    }
}
