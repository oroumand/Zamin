using FluentValidation;
using Zamin.Extensions.Translations.Abstractions;

namespace MiniBlog.Core.RequestResponse.Blogs.Queries.GetById;

public class GetBlogByIdQueryValidator : AbstractValidator<GetBlogByIdQuery>
{
    public GetBlogByIdQueryValidator(ITranslator translator)
    {
        RuleFor(query => query.BlogId)
            .NotEmpty()
            .WithMessage(translator["Required", nameof(GetBlogByIdQuery.BlogId)]);
    }
}