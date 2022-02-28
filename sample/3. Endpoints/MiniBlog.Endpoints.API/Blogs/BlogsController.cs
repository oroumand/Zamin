using Microsoft.AspNetCore.Mvc;
using MiniBlog.Core.Contracts.Blogs.Commands.CreateBlog;
using Zamin.EndPoints.Web.Controllers;
using Zamin.Utilities.Services.Chaching;

namespace MiniBlog.Endpoints.API.Blogs
{
    [Route("api/[controller]")]
    public class BlogsssController : BaseController
    {
        private readonly ICacheAdapter _cacheAdapter;

        public BlogsssController(ICacheAdapter cacheAdapter)
        {
            _cacheAdapter = cacheAdapter;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateBlogCommand createBlog)
        {
            if (_cacheAdapter.Get<string>("distributed-cache-test") == null)
                _cacheAdapter.Add<string>("distributed-cache-test", Guid.NewGuid().ToString(), null, null);

            return await Create(createBlog);
        }
    }
}
