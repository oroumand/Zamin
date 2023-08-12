using Zamin.Core.RequestResponse.Commands;

namespace MiniBlog.Core.RequestResponse.Blogs.Commands.Delete;

public class DeleteBlogCommand : ICommand
{
    public int Id { get; set; }
}