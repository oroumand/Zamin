using Microsoft.AspNetCore.Mvc;
using MiniBlog.Core.Contracts.Blogs.Commands.CreateBlog;
using Zamin.EndPoints.Web.Controllers;

namespace MiniBlog.Endpoints.API.Blogs
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Post(CreateBlogCommand createBlog )
        {
            return await Create(createBlog);
        }
    }
}
