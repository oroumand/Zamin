using FluentValidation;
using System;
using Zamin.MiniBlog.Core.Domain;
using Zamin.Utilities.Services.Localizations;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.CreatePerson
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator(ITranslator translator)
        {
            RuleFor(command => command.FirstName)
                .NotEmpty()
                .WithMessage(translator[
                    ZaminMiniBlogValidationResources.ValidationErrorRequired,
                    ZaminMiniBlogResources.FirstName]);

            RuleFor(command => command.LastName)
                .NotEmpty()
                .WithMessage(translator[
                    ZaminMiniBlogValidationResources.ValidationErrorRequired,
                    ZaminMiniBlogResources.LastName]);

            When(command => command.BirthDate.HasValue, () =>
            {
                RuleFor(command => command.BirthDate)
                     .Cascade(CascadeMode.Stop)
                     .NotEmpty()
                     .WithMessage(translator[
                         ZaminMiniBlogValidationResources.ValidationErrorRequired,
                         ZaminMiniBlogResources.BirthDate])
                     .LessThan(DateTime.Now);
            });

        }
    }
}
