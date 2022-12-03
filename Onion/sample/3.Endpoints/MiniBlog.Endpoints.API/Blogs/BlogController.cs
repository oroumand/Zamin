using Microsoft.AspNetCore.Mvc;
using MiniBlog.Core.Contracts.Blogs.Commands.CreateBlog;
using MiniBlog.Core.Contracts.Blogs.Commands.DeleteBlog;
using MiniBlog.Core.Contracts.Blogs.Queries.GetBlogByBusinessId;
using Zamin.EndPoints.Web.Controllers;

namespace MiniBlog.Endpoints.API.Blogs
{
    [Route("api/[controller]")]
    public class BlogController : BaseController
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateBlog([FromBody] CreateBlogCommand command)
            => await Create<CreateBlogCommand, Guid>(command);

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteBlog([FromQuery] DeleteBlogCommand command)
            => await Delete(command);

        [HttpGet("[action]")]
        public async Task<IActionResult> GetBlogByBusinessId([FromQuery] GetBlogByBusinessIdQuery query)
            => await Query<GetBlogByBusinessIdQuery, BlogQr>(query);
    }
}
