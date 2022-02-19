using FluentValidation;
using MiniBlog.Core.Contracts.IgnoreAuditableEntities.Commands;

namespace MiniBlog.Core.ApplicationService.IgnoreAuditableEntities.Commands.UpdateIgnoreAuditableEntitiy
{
    public class UpdateIgnoreAuditableEntitiyValidator : AbstractValidator<UpdateIgnoreAuditableEntityCommand>
    {
        public UpdateIgnoreAuditableEntitiyValidator()
        {
        }
    }
}
