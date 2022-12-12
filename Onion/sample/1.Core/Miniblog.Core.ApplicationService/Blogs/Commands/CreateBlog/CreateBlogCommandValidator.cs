using FluentValidation;
using MiniBlog.Core.Contracts.Blogs.Commands.CreateBlog;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Extensions.Translations.Abstractions;

namespace Miniblog.Core.ApplicationService.Blogs.Commands.CreateBlog
{
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogCommandValidator(ITranslator translator)
        {
            RuleFor(c => c.Title)
                .NotNull().WithMessage(translator["Required", nameof(Title)])
                .MinimumLength(10).WithMessage(translator["MinimumLength", nameof(Title), "2"])
                .MaximumLength(100).WithMessage(translator["MaximumLength", nameof(Title), "100"]);

            RuleFor(c => c.Description)
                .NotNull().WithMessage(translator["Required", nameof(Description)]).WithErrorCode("1")
                .MinimumLength(50).WithMessage(translator["MinimumLength", nameof(Description), "10"]).WithErrorCode("2")
                .MaximumLength(500).WithMessage(translator["MaximumLength", nameof(Description), "500"]).WithErrorCode("3");
        }
    }
}
