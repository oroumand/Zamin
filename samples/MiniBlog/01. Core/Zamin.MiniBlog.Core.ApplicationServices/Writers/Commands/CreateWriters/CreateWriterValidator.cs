using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zamin.MiniBlog.Core.ApplicationServices.Writers.Commands.CreatePerson
{
    public class CreateWriterValidator:AbstractValidator<CreateWiterCommand>
    {
        public CreateWriterValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("اسمش خالیه")
                .MinimumLength(5).WithMessage("کمه")
                .MaximumLength(100).WithMessage("زیاده");
        }
    }
}
