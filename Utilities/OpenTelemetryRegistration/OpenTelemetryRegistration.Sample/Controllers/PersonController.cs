using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenTelemetryRegistration.Sample.Models;

namespace OpenTelemetryRegistration.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonContext context;

        public PersonController(PersonContext context)
        {
            this.context = context;
        }
        [HttpGet(Name = "GetPerson")]
        public async Task<ActionResult> Get()
        {
            return Ok(await context.People.ToListAsync());
        }
        [HttpPost(Name = "SavePerson")]

        public async Task<IActionResult> Save([FromBody]Person person)
        {
            await context.People.AddAsync(person);
            await context.SaveChangesAsync();
            return Ok(person.Id);
        }
    }
}
