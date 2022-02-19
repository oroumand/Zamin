using Microsoft.AspNetCore.Mvc;
using MiniBlog.Core.Contracts.SoftDeleteEntities.Commands;
using Zamin.EndPoints.Web.Controllers;

namespace MiniBlog.Endpoints.API.SoftDeleteEntities.Controllers
{
    [Route("api/[controller]")]
    public class SoftDeleteEntitiyController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSoftDeleteEntitiyCommand command)
            => await Create<CreateSoftDeleteEntitiyCommand, Guid>(command);

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteSoftDeleteEntitiyCommand command)
            => await Delete<DeleteSoftDeleteEntitiyCommand>(command);
    }
}
