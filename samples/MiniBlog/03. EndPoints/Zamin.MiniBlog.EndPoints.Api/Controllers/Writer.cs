using Zamin.MiniBlog.Core.ApplicationServices.Writers.Commands.CreateWriters;
using Microsoft.AspNetCore.Mvc;

namespace Zamin.MiniBlog.EndPoints.Api.Controllers
{
    [Route("api/[Controller]")]
    public class WriterController : BaseController
    {
        [HttpPost("Save")]
        public async Task<IActionResult> Post([FromBody] CreateWiterCommand createWtirer)
        {
            return await Create<CreateWiterCommand, long>(createWtirer);
        }
    }
}
