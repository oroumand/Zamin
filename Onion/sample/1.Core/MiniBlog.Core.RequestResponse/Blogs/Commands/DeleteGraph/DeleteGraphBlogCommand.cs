using Zamin.Core.RequestResponse.Commands;

namespace MiniBlog.Core.RequestResponse.Blogs.Commands.DeleteGraph;

public class DeleteGraphBlogCommand : ICommand
{
    public int Id { get; set; }
}