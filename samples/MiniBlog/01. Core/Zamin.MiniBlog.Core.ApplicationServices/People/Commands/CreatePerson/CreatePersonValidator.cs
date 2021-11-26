namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.CreatePerson;

public class CreatePersonValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage("اسمش خالیه")
            .MinimumLength(5).WithMessage("کمه")
            .MaximumLength(100).WithMessage("زیاده");
    }
}