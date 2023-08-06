using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.Contracts.Blogs.Commands.Update;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Contracts.ApplicationServices.Commands;
using Zamin.Core.Domain.Exceptions;
using Zamin.Utilities;

namespace MiniBlog.Core.ApplicationService.Blogs.Commands.Update;

public sealed class UpdateBlogCommandHandler : CommandHandler<UpdateBlogCommand>
{
    private readonly IBlogCommandRepository _blogCommandRepository;

    public UpdateBlogCommandHandler(ZaminServices zaminServices,
                                    IBlogCommandRepository blogCommandRepository) : base(zaminServices)
    {
        _blogCommandRepository = blogCommandRepository;
    }

    public override async Task<CommandResult> Handle(UpdateBlogCommand command)
    {
        var blog = await _blogCommandRepository.GetAsync(command.Id);

        if (blog is null)
            throw new InvalidEntityStateException("بلاگ یافت نشد");

        blog.Update(command.Title, command.Description);

        await _blogCommandRepository.CommitAsync();

        return Ok();
    }
}
