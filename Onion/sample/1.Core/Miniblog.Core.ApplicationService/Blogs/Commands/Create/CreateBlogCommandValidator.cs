using FluentValidation;
using MiniBlog.Core.Contracts.Blogs.Commands.Create;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Extensions.Translations.Abstractions;

namespace MiniBlog.Core.ApplicationService.Blogs.Commands.Create
{
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogCommandValidator(ITranslator translator)
        {
            RuleFor(c => c.Title)
                .NotNull().WithMessage(translator["Required", nameof(Title)])
                .MinimumLength(2).WithMessage(translator["MinimumLength", nameof(Title), "2"])
                .MaximumLength(100).WithMessage(translator["MaximumLength", nameof(Title), "100"]);

            RuleFor(c => c.Description)
                .NotNull().WithMessage(translator["Required", nameof(Description)]).WithErrorCode("1")
                .MinimumLength(10).WithMessage(translator["MinimumLength", nameof(Description), "10"]).WithErrorCode("2")
                .MaximumLength(500).WithMessage(translator["MaximumLength", nameof(Description), "500"]).WithErrorCode("3");
        }
    }
}
