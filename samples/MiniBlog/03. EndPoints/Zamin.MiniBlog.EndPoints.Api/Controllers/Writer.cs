using Microsoft.AspNetCore.Mvc;
using Zamin.Core.Domain.Data;
using Zamin.MiniBlog.Core.ApplicationServices.Writers.Commands.CreateWriters;
using Zamin.MiniBlog.Core.ApplicationServices.Writers.Queries.UserByFirstName;
using Zamin.MiniBlog.Core.Domain.Writers.QueryModels;

namespace Zamin.MiniBlog.EndPoints.Api.Controllers
{
    [Route("api/[Controller]")]
    public class WriterController : BaseController
    {
        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] UserByFirstNameQuery userByFirstNameQuery)
        {
            return await Query<UserByFirstNameQuery, PagedData<WriterSummary>>(userByFirstNameQuery);
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Post([FromBody] CreateWiterCommand createWtirer)
        {
            return await Create<CreateWiterCommand, long>(createWtirer);
        }
    }
}
