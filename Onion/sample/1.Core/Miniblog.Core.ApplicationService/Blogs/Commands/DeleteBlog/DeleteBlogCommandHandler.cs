using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.Contracts.Blogs.Commands.DeleteBlog;
using MiniBlog.Core.Domain.Blogs.Entities;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Contracts.ApplicationServices.Commands;
using Zamin.Core.Domain.Exceptions;
using Zamin.Utilities;

namespace MiniBlog.Core.ApplicationService.Blogs.Commands.DeleteBlog;

public class DeleteBlogCommandHandler : CommandHandler<DeleteBlogCommand>
{
    private readonly IBlogCommandRepository _blogCommandRepository;

    public DeleteBlogCommandHandler(ZaminServices zaminServices,
                                    IBlogCommandRepository blogCommandRepository) : base(zaminServices)
    {
        _blogCommandRepository = blogCommandRepository;
    }

    public override async Task<CommandResult> Handle(DeleteBlogCommand command)
    {
        Blog blog = await _blogCommandRepository.GetAsync(command.BlogBusinessId);

        if (blog is null)
            throw new InvalidEntityStateException("Blog Not Exists");

        _blogCommandRepository.Delete(blog);

        await _blogCommandRepository.CommitAsync();

        return Ok();
    }
}
