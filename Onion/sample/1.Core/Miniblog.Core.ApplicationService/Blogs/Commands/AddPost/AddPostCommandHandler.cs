using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.RequestResponse.Blogs.Commands.AddPost;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace MiniBlog.Core.ApplicationService.Blogs.Commands.AddPost;

public sealed class AddPostCommandHandler : CommandHandler<AddPostCommand>
{
    private readonly IBlogCommandRepository _blogCommandRepository;

    public AddPostCommandHandler(ZaminServices zaminServices,
                                 IBlogCommandRepository blogCommandRepository) : base(zaminServices)
    {
        _blogCommandRepository = blogCommandRepository;
    }

    public override async Task<CommandResult> Handle(AddPostCommand command)
    {
        var blog = await _blogCommandRepository.GetGraphAsync(command.BlogId);

        if (blog is null)
            throw new InvalidEntityStateException("بلاگ یافت نشد");

        blog.AddPost(command.Title);

        await _blogCommandRepository.CommitAsync();

        return Ok();
    }
}
