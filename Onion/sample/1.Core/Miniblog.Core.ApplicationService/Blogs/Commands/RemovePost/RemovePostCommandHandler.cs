using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.RequestResponse.Blogs.Commands.RemovePost;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace MiniBlog.Core.ApplicationService.Blogs.Commands.RemovePost;

public sealed class RemovePostCommandHandler : CommandHandler<RemovePostCommand>
{
    private readonly IBlogCommandRepository _blogCommandRepository;

    public RemovePostCommandHandler(ZaminServices zaminServices,
                                    IBlogCommandRepository blogCommandRepository) : base(zaminServices)
    {
        _blogCommandRepository = blogCommandRepository;
    }

    public override async Task<CommandResult> Handle(RemovePostCommand command)
    {
        var blog = await _blogCommandRepository.GetGraphAsync(command.BlogId);

        if (blog is null)
            throw new InvalidEntityStateException("بلاگ یافت نشد");

        blog.RemovePost(command.PostId);

        await _blogCommandRepository.CommitAsync();

        return Ok();
    }
}
