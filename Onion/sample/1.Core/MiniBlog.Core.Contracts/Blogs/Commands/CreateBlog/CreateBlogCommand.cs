using Zamin.Core.Contracts.ApplicationServices.Commands;

namespace MiniBlog.Core.Contracts.Blogs.Commands.CreateBlog;

public class CreateBlogCommand : ICommand<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; }= string.Empty;
}