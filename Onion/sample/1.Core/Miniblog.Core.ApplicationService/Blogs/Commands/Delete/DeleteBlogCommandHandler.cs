using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.RequestResponse.Blogs.Commands.Delete;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Contracts.Data.Commands;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace MiniBlog.Core.ApplicationService.Blogs.Commands.Delete;

public sealed class DeleteBlogCommandHandler(ZaminServices zaminServices,
                                IBlogCommandRepository blogCommandRepository,
                                IUnitOfWork blogUnitOfWork) : CommandHandler<DeleteBlogCommand>(zaminServices)
{
    private readonly IUnitOfWork _blogUnitOfWork = blogUnitOfWork;
    private readonly IBlogCommandRepository _blogCommandRepository = blogCommandRepository;

    public override async Task<CommandResult> Handle(DeleteBlogCommand command)
    {
        var blog = await _blogCommandRepository.GetGraphAsync(command.Id) ?? throw new InvalidEntityStateException("بلاگ یافت نشد");
        
        blog.Delete();

        _blogCommandRepository.Delete(blog);

        await _blogCommandRepository.CommitAsync();

        return Ok();
    }
}
