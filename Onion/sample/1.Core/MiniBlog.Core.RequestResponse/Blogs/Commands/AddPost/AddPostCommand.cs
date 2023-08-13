using System.Net;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace MiniBlog.Core.RequestResponse.Blogs.Commands.AddPost;

public class AddPostCommand : ICommand, IWebRequest
{
    public int BlogId { get; set; }
    public string Title { get; set; } = string.Empty;

    public string Path => "/api/Blog/Create";
}

