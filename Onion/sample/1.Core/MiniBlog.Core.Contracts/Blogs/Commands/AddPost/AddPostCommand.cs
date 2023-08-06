using Zamin.Core.Contracts.ApplicationServices.Commands;

namespace MiniBlog.Core.Contracts.Blogs.Commands.AddPost;

public class AddPostCommand : ICommand
{
    public int BlogId { get; set; }
    public string Title { get; set; } = string.Empty;
}