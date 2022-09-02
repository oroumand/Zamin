using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.Contracts.Blogs.Commands.CreateBlog;
using MiniBlog.Core.Domain.Blogs.Entities;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Contracts.ApplicationServices.Commands;
using Zamin.Core.Domain.Exceptions;
using Zamin.Utilities;

namespace Miniblog.Core.ApplicationService.Blogs.Commands.CreateBlog
{
    public class CreateBlogCommandHandler : CommandHandler<CreateBlogCommand,Guid>
    {

        private readonly IBlogCommandRepository _blogCommandRepository;

        public CreateBlogCommandHandler(ZaminServices zaminServices,
                                        IBlogCommandRepository blogCommandRepository) : base(zaminServices)
        {
            _blogCommandRepository = blogCommandRepository;
        }

        public override async Task<CommandResult<Guid>> Handle(CreateBlogCommand command)
        {
            Blog blog = Blog.Create(command.Title, command.Description);
            await _blogCommandRepository.InsertAsync(blog);
            await _blogCommandRepository.CommitAsync();
            return Ok(blog.BusinessId.Value);
        }
    }
}
