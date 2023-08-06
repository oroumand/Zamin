using Zamin.Core.Contracts.ApplicationServices.Commands;

namespace MiniBlog.Core.Contracts.Blogs.Commands.RemovePost;

public class RemovePostCommand : ICommand
{
    public int BlogId { get; set; }
    public int PostId { get; set; }
}