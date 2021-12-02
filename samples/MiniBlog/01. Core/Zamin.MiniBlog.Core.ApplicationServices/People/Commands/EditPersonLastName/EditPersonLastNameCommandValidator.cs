using FluentValidation;
using Zamin.MiniBlog.Core.ApplicationServices.Common.Validators;
using Zamin.MiniBlog.Core.Domain;
using Zamin.MiniBlog.Core.Domain.People.Entities;
using Zamin.MiniBlog.Core.Domain.People.Repositories;
using Zamin.Utilities.Services.Localizations;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.EditPersonLastName
{
    public class EditPersonLastNameCommandValidator : AbstractValidator<EditPersonLastNameCommand>
    {

        public EditPersonLastNameCommandValidator(
            ITranslator translator,
            IPersonCommandRepository personCommandRepository)
        {
            RuleFor(command => command.BusinessId)
                .SetValidator(new BusinessIdValidator<Person>(
                    translator,
                    personCommandRepository,
                    ZaminMiniBlogResources.BusinessId));

            RuleFor(command => command.LastName)
                .NotEmpty()
                .WithMessage(translator[
                    ZaminMiniBlogValidationResources.ValidationErrorRequired,
                    ZaminMiniBlogResources.LastName]);
        }
    }
}
