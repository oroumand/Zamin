using FluentValidation;
using MiniBlog.Core.Contracts.Blogs.Queries.GetById;
using Zamin.Extensions.Translations.Abstractions;

namespace MiniBlog.Core.ApplicationService.Blogs.Queries.GetById;

public class GetBlogByIdQueryValidator : AbstractValidator<GetBlogByIdQuery>
{
    public GetBlogByIdQueryValidator(ITranslator translator)
    {
        RuleFor(query => query.BlogId)
            .NotEmpty()
            .WithMessage(translator["Required", nameof(GetBlogByIdQuery.BlogId)]);
    }
}