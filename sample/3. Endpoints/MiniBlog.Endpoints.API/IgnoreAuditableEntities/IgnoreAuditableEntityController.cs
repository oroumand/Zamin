using Microsoft.AspNetCore.Mvc;
using MiniBlog.Core.Contracts.IgnoreAuditableEntities.Commands;
using Zamin.EndPoints.Web.Controllers;

namespace MiniBlog.Endpoints.API.IgnoreAuditableEntities
{
    [Route("api/[controller]")]
    public class IgnoreAuditableEntityController : BaseController
    {
        [HttpPost("Create")]
        public async Task<IActionResult> CreateIgnoreAuditableEntity([FromBody] CreateIgnoreAuditableEntityCommand command)
            => await Create<CreateIgnoreAuditableEntityCommand, Guid>(command);

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateIgnoreAuditableEntityCommand([FromBody] UpdateIgnoreAuditableEntityCommand command)
            => await Edit<UpdateIgnoreAuditableEntityCommand>(command);
    }
}
