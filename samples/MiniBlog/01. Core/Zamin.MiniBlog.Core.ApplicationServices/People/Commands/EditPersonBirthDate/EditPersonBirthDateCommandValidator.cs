using FluentValidation;
using System;
using Zamin.MiniBlog.Core.ApplicationServices.Common.Validators;
using Zamin.MiniBlog.Core.Domain;
using Zamin.MiniBlog.Core.Domain.People.Entities;
using Zamin.MiniBlog.Core.Domain.People.Repositories;
using Zamin.Utilities.Services.Localizations;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.EditPersonBirthDate
{
    public class EditPersonBirthDateCommandValidator : AbstractValidator<EditPersonBirthDateCommand>
    {
        public EditPersonBirthDateCommandValidator(
            ITranslator translator,
            IPersonCommandRepository personCommandRepository)
        {
            RuleFor(command => command.BusinessId)
                .SetValidator(new BusinessIdValidator<Person>(
                    translator,
                    personCommandRepository,
                    ZaminMiniBlogResources.BusinessId));

            RuleFor(command => command.BirthDate)
                 .Cascade(CascadeMode.Stop)
                 .NotEmpty()
                 .WithMessage(translator[
                     ZaminMiniBlogValidationResources.ValidationErrorRequired,
                     ZaminMiniBlogResources.BirthDate])
                 .LessThan(DateTime.Now)
                 .WithMessage("");
        }
    }
}
