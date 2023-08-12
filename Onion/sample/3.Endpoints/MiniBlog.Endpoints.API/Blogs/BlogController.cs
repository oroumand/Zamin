using Microsoft.AspNetCore.Mvc;
using MiniBlog.Core.RequestResponse.Blogs.Commands.AddPost;
using MiniBlog.Core.RequestResponse.Blogs.Commands.Create;
using MiniBlog.Core.RequestResponse.Blogs.Commands.Delete;
using MiniBlog.Core.RequestResponse.Blogs.Commands.DeleteGraph;
using MiniBlog.Core.RequestResponse.Blogs.Commands.RemovePost;
using MiniBlog.Core.RequestResponse.Blogs.Commands.Update;
using MiniBlog.Core.RequestResponse.Blogs.Queries.GetById;
using Zamin.EndPoints.Web.Controllers;

namespace MiniBlog.Endpoints.API.Blogs
{
    [Route("api/[controller]")]
    public class BlogController : BaseController
    {
        #region Commands
        [HttpPost("Create")]
        public async Task<IActionResult> CreateBlog([FromBody] CreateBlogCommand command) => await Create<CreateBlogCommand, Guid>(command);

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateBlog([FromBody] UpdateBlogCommand command) => await Edit(command);

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteBlog([FromBody] DeleteBlogCommand command) => await Delete(command);

        [HttpDelete("DeleteGraph")]
        public async Task<IActionResult> DeleteGraphBlog([FromBody] DeleteGraphBlogCommand command) => await Delete(command);

        [HttpPost("AddPost")]
        public async Task<IActionResult> AddPost([FromBody] AddPostCommand command) => await Create(command);

        [HttpDelete("RemovePost")]
        public async Task<IActionResult> RemovePost([FromBody] RemovePostCommand command) => await Delete(command);
        #endregion

        #region Queries
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(GetBlogByIdQuery query) => await Query<GetBlogByIdQuery, BlogQr?>(query);
        #endregion

        #region Methods
        [HttpGet("/Clear")]
        public bool Clear()
        {
            GC.Collect(2);
            return true;
        }
        #endregion
    }
}
