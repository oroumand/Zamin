using Zamin.Core.Contracts.ApplicationServices.Commands;

namespace MiniBlog.Core.Contracts.Blogs.Commands.DeleteGraph;

public class DeleteGraphBlogCommand : ICommand
{
    public int Id { get; set; }
}