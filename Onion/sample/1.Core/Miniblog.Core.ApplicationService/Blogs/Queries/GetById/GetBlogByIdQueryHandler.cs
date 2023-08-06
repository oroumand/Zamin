using MiniBlog.Core.Contracts.Blogs.Queries;
using MiniBlog.Core.Contracts.Blogs.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.Contracts.ApplicationServices.Queries;
using Zamin.Utilities;

namespace MiniBlog.Core.ApplicationService.Blogs.Queries.GetById;

public class GetBlogByIdQueryHandler : QueryHandler<GetBlogByIdQuery, BlogQr?>
{
    private readonly IBlogQueryRepository _blogQueryRepository;

    public GetBlogByIdQueryHandler(ZaminServices zaminServices,
                                   IBlogQueryRepository blogQueryRepository) : base(zaminServices)
    {
        _blogQueryRepository = blogQueryRepository;
    }

    public override async Task<QueryResult<BlogQr?>> Handle(GetBlogByIdQuery query)
    {
        var blog = await _blogQueryRepository.ExecuteAsync(query);

        return Result(blog);
    }
}
