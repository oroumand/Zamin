using Zamin.Core.Contracts.ApplicationServices.Commands;

namespace MiniBlog.Core.Contracts.Blogs.Commands.DeleteBlog;

public class DeleteBlogCommand : ICommand
{
    public Guid BlogBusinessId { get; set; }
}
