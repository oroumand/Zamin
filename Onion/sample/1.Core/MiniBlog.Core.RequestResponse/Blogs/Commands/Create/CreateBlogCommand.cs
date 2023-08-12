using Zamin.Core.RequestResponse.Commands;

namespace MiniBlog.Core.RequestResponse.Blogs.Commands.Create;

public class CreateBlogCommand : ICommand<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}