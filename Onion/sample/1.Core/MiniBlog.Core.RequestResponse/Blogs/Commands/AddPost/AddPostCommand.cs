using Zamin.Core.RequestResponse.Commands;

namespace MiniBlog.Core.RequestResponse.Blogs.Commands.AddPost;

public class AddPostCommand : ICommand
{
    public int BlogId { get; set; }
    public string Title { get; set; } = string.Empty;
}