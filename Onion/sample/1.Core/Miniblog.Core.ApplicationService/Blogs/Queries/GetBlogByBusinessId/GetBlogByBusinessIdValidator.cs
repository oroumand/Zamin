using FluentValidation;
using MiniBlog.Core.Contracts.Blogs.Queries.GetBlogByBusinessId;
using Zamin.Extensions.Translations.Abstractions;

namespace MiniBlog.Core.ApplicationService.Blogs.Queries.GetBlogByBusinessId;

public class GetBlogByBusinessIdValidator : AbstractValidator<GetBlogByBusinessIdQuery>
{
    public GetBlogByBusinessIdValidator(ITranslator translator)
    {
        RuleFor(query => query.BlogBusinessId)
            .NotEmpty()
            .WithMessage(translator["Required", nameof(GetBlogByBusinessIdQuery.BlogBusinessId)]);
    }
}