using MiniBlog.Core.Contracts.Blogs.CommandRepositories;
using MiniBlog.Core.Contracts.Blogs.Commands.CreateBlog;
using MiniBlog.Core.Domain.Blogs.Entities;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Contracts.ApplicationServices.Commands;
using Zamin.Utilities;

namespace Miniblog.Core.ApplicationService.Blogs.Commands.CreateBlog
{
    public class CreateBlogCommandHandler : CommandHandler<CreateBlogCommand>
    {

        private readonly IBlogCommandRepository _blogCommandRepository;

        public CreateBlogCommandHandler(ZaminServices zaminServices, IBlogCommandRepository blogCommandRepository) : base(zaminServices)
        {
            _blogCommandRepository = blogCommandRepository;
        }

        public override async Task<CommandResult> Handle(CreateBlogCommand request)
        {
            Blog blog = new(request.BusunessId, request.Title, request.Description);
            await _blogCommandRepository.InsertAsync(blog);
            await _blogCommandRepository.CommitAsync();
            return Ok();

        }
    }
}
