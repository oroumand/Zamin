using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace MiniBlog.Core.RequestResponse.Blogs.Commands.AddBlogPost;

public class AddBlogPostCommand : ICommand<Guid>, IWebRequest
{
    public string BlogTitle { get; set; } = string.Empty;
    public string BlogDescription { get; set; } = string.Empty;
    public string PostTitle { get; set; } = string.Empty;

    public string Path => "/api/Blog/AddBlogPost";
}

