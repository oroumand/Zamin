using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace MiniBlog.Core.RequestResponse.Blogs.Commands.Delete;

public class DeleteBlogCommand : ICommand, IWebRequest
{
    public int Id { get; set; }

    public string Path => "/api/Blog/Delete";
}