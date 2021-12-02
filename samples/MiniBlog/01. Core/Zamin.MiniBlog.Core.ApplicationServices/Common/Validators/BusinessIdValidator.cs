using FluentValidation;
using System;
using Zamin.Core.Domain.Data;
using Zamin.Core.Domain.Entities;
using Zamin.Core.Domain.ValueObjects;
using Zamin.MiniBlog.Core.Domain;
using Zamin.Utilities.Services.Localizations;

namespace Zamin.MiniBlog.Core.ApplicationServices.Common.Validators
{
    public class BusinessIdValidator<TEntity> : AbstractValidator<Guid> where TEntity : AggregateRoot
    {
        public BusinessIdValidator(
            ITranslator translator,
            ICommandRepository<TEntity> commandRepository,
            string AggregateRootName)
        {
            RuleFor(businessId => businessId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(translator[
                    ZaminMiniBlogValidationResources.ValidationErrorRequired,
                    ZaminMiniBlogResources.BusinessId])
                .Must(businessId => commandRepository.Exists(c => c.BusinessId == BusinessId.FromGuid(businessId)))
                .WithMessage(translator[
                    ZaminMiniBlogValidationResources.ValidationErrorRequired,
                    ZaminMiniBlogResources.BusinessId,
                    AggregateRootName]);
        }
    }
}
