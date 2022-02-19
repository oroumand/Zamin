using FluentValidation;
using MiniBlog.Core.Contracts.IgnoreAuditableEntities.Commands;

namespace MiniBlog.Core.ApplicationService.IgnoreAuditableEntities.Commands.CreateIgnoreAuditableEntitiy
{
    public class CreateIgnoreAuditableEntitiyValidator : AbstractValidator<CreateIgnoreAuditableEntityCommand>
    {
        public CreateIgnoreAuditableEntitiyValidator()
        {
        }
    }
}
