using FluentValidation;
using Zamin.Extensions.Translations.Abstractions;

namespace MiniBlog.Core.RequestResponse.Blogs.Commands.Create
{
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogCommandValidator(ITranslator translator)
        {
            RuleFor(c => c.Title)
                .NotNull().WithMessage(translator["Required", "Title"])
                .MinimumLength(2).WithMessage(translator["MinimumLength", "Title", "2"])
                .MaximumLength(100).WithMessage(translator["MaximumLength", "Title", "100"]);

            RuleFor(c => c.Description)
                .NotNull().WithMessage(translator["Required", "Description"]).WithErrorCode("1")
                .MinimumLength(10).WithMessage(translator["MinimumLength", "Description", "10"]).WithErrorCode("2")
                .MaximumLength(500).WithMessage(translator["MaximumLength", "Description", "500"]).WithErrorCode("3");
        }
    }
}
