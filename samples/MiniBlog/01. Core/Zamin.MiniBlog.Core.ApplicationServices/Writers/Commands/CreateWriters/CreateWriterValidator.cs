﻿namespace Zamin.MiniBlog.Core.ApplicationServices.Writers.Commands.CreateWriters;

public class CreateWriterValidator : AbstractValidator<CreateWiterCommand>
{
    public CreateWriterValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage("اسمش خالیه")
            .MinimumLength(5).WithMessage("کمه")
            .MaximumLength(100).WithMessage("زیاده");
    }
}
