using MiniBlog.Core.Contracts.IgnoreAuditableEntities.Commands;
using MiniBlog.Core.Contracts.IgnoreAuditableEntities.Repositories;
using MiniBlog.Core.Domain.IgnoreAuditableEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Contracts.ApplicationServices.Commands;
using Zamin.Utilities;

namespace MiniBlog.Core.ApplicationService.IgnoreAuditableEntities.Commands.CreateIgnoreAuditableEntitiy
{
    public class CreateIgnoreAuditableEntitiyHandler : CommandHandler<CreateIgnoreAuditableEntityCommand, Guid>
    {
        private readonly IIgnoreAuditableEntitiyCommandRepository _commandRepository;

        public CreateIgnoreAuditableEntitiyHandler(
            ZaminServices zaminServices,
            IIgnoreAuditableEntitiyCommandRepository commandRepository) : base(zaminServices)
        {
            _commandRepository = commandRepository;
        }

        public override async Task<CommandResult<Guid>> Handle(CreateIgnoreAuditableEntityCommand command)
        {
            var ignoreAuditableEntitiy = new IgnoreAuditableEntity(command.Name);

            await _commandRepository.InsertAsync(ignoreAuditableEntitiy);

            await _commandRepository.CommitAsync();

            return await OkAsync(ignoreAuditableEntitiy.BusinessId.Value);
        }
    }
}
