using FluentValidation;
using MiniBlog.Core.Contracts.SoftDeleteEntities.Commands;
using MiniBlog.Core.Contracts.SoftDeleteEntities.Repositories;
using Zamin.Core.Domain.ValueObjects;

namespace MiniBlog.Core.ApplicationService.SoftDeleteEntities.Commands.DeleteSoftDeleteEntitiy
{
    public class DeleteSoftDeleteEntitiyValidator : AbstractValidator<DeleteSoftDeleteEntitiyCommand>
    {
        public DeleteSoftDeleteEntitiyValidator(ISoftDeleteEntitiyCommandRepository commandRepository)
        {
            RuleFor(c=>c.SoftDeleteEntitiyBuseinessId)
                .NotEmpty()
                .Must(guid => commandRepository.Exists(c=>c.BusinessId == BusinessId.FromGuid(guid))).WithMessage("NotExists");
        }
    }
}
