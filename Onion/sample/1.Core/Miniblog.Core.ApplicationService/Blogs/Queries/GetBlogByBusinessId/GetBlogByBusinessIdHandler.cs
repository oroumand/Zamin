using MiniBlog.Core.Contracts.Blogs.Queries;
using MiniBlog.Core.Contracts.Blogs.Queries.GetBlogByBusinessId;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.Contracts.ApplicationServices.Queries;
using Zamin.Core.Domain.Exceptions;
using Zamin.Utilities;

namespace MiniBlog.Core.ApplicationService.Blogs.Queries.GetBlogByBusinessId;

public class GetBlogByBusinessIdHandler : QueryHandler<GetBlogByBusinessIdQuery, BlogQr>
{
    private readonly IBlogQueryRepository _blogQueryRepository;

    public GetBlogByBusinessIdHandler(ZaminServices zaminServices,
                                      IBlogQueryRepository blogQueryRepository) : base(zaminServices)
    {
        _blogQueryRepository = blogQueryRepository;
    }

    public override async Task<QueryResult<BlogQr>> Handle(GetBlogByBusinessIdQuery query)
    {
        var blog = await _blogQueryRepository.Execute(query);

        return Result(blog);
    }
}
