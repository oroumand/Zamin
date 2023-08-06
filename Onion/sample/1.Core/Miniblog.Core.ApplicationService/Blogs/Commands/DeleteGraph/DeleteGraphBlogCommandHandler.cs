using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.Contracts.Blogs.Commands.DeleteGraph;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Contracts.ApplicationServices.Commands;
using Zamin.Core.Domain.Exceptions;
using Zamin.Utilities;

namespace MiniBlog.Core.ApplicationService.Blogs.Commands.DeleteGraph;

public sealed class DeleteGraphBlogCommandHandler : CommandHandler<DeleteGraphBlogCommand>
{
    private readonly IBlogCommandRepository _blogCommandRepository;

    public DeleteGraphBlogCommandHandler(ZaminServices zaminServices,
                                    IBlogCommandRepository blogCommandRepository) : base(zaminServices)
    {
        _blogCommandRepository = blogCommandRepository;
    }

    public override async Task<CommandResult> Handle(DeleteGraphBlogCommand command)
    {
        var blog = await _blogCommandRepository.GetAsync(command.Id);

        if (blog is null)
            throw new InvalidEntityStateException("بلاگ یافت نشد");

        blog.DeleteGraph();

        _blogCommandRepository.DeleteGraph(blog.Id);

        await _blogCommandRepository.CommitAsync();

        return Ok();
    }
}