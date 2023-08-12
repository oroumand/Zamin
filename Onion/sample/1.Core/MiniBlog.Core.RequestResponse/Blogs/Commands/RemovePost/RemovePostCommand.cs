using Zamin.Core.RequestResponse.Commands;

namespace MiniBlog.Core.RequestResponse.Blogs.Commands.RemovePost;

public class RemovePostCommand : ICommand
{
    public int BlogId { get; set; }
    public int PostId { get; set; }
}