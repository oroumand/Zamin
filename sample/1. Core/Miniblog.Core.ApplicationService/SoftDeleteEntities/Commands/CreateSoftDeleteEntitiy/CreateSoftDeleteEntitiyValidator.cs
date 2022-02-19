using FluentValidation;
using MiniBlog.Core.Contracts.SoftDeleteEntities.Commands;

namespace MiniBlog.Core.ApplicationService.SoftDeleteEntities.Commands.CreateSoftDeleteEntitiy
{
    public class CreateSoftDeleteEntitiyValidator : AbstractValidator<CreateSoftDeleteEntitiyCommand>
    {
        public CreateSoftDeleteEntitiyValidator()
        {
        }
    }
}
