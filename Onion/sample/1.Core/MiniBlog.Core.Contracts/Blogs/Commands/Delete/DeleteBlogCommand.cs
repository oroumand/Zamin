using Zamin.Core.Contracts.ApplicationServices.Commands;

namespace MiniBlog.Core.Contracts.Blogs.Commands.Delete;

public class DeleteBlogCommand : ICommand
{
    public int Id { get; set; }
}