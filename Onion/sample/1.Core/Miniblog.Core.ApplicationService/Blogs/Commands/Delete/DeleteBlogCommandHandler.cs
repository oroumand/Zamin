using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.Contracts.Blogs.Commands.Delete;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Contracts.ApplicationServices.Commands;
using Zamin.Core.Domain.Exceptions;
using Zamin.Utilities;

namespace MiniBlog.Core.ApplicationService.Blogs.Commands.Delete;

public sealed class DeleteBlogCommandHandler : CommandHandler<DeleteBlogCommand>
{
    private readonly IBlogCommandRepository _blogCommandRepository;

    public DeleteBlogCommandHandler(ZaminServices zaminServices,
                                    IBlogCommandRepository blogCommandRepository) : base(zaminServices)
    {
        _blogCommandRepository = blogCommandRepository;
    }

    public override async Task<CommandResult> Handle(DeleteBlogCommand command)
    {
        var blog = await _blogCommandRepository.GetGraphAsync(command.Id);

        if (blog is null)
            throw new InvalidEntityStateException("بلاگ یافت نشد");

        blog.Delete();

        _blogCommandRepository.Delete(blog);

        await _blogCommandRepository.CommitAsync();

        return Ok();
    }
}
