using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.Domain.Blogs.Entities;
using MiniBlog.Core.RequestResponse.Blogs.Commands.AddBlogPost;
using MiniBlog.Core.RequestResponse.Blogs.Commands.AddPost;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Contracts.Data.Commands;
using Zamin.Core.Domain.Exceptions;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace MiniBlog.Core.ApplicationService.Blogs.Commands.AddBlogPost;

public sealed class AddBlogPostCommandHandler(ZaminServices zaminServices,
                                 IBlogCommandRepository _blogCommandRepository,
                                 IUnitOfWork _unitOfWork) : CommandHandler<AddBlogPostCommand, Guid>(zaminServices)
{
    public override async Task<CommandResult<Guid>> Handle(AddBlogPostCommand command)
    {
        _unitOfWork.BeginTransaction();

        Blog blog = Blog.Create(command.BlogTitle, command.BlogDescription);
        await _blogCommandRepository.InsertAsync(blog);
        await _unitOfWork.CommitAsync();

        blog.AddPost(command.PostTitle);
        await _unitOfWork.CommitAsync();

        _unitOfWork.CommitTransaction();

        return Ok(blog.BusinessId.Value);
    }
}
