using Microsoft.AspNetCore.Mvc;
using MiniBlog.Core.Contracts.Blogs.Commands.CreateBlog;
using Zamin.EndPoints.Web.Controllers;

namespace MiniBlog.Endpoints.API.Blogs
{
    [Route("api/[controller]")]
    public class BlogsssController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Post(CreateBlogCommand createBlog )
        {
            return await Create(createBlog);
        }
    }
}
