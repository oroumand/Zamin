using Microsoft.AspNetCore.Mvc;
using Zamin.Core.Domain.Data;
using Zamin.MiniBlog.Core.ApplicationServices.People.Commands.CreatePerson;
using Zamin.MiniBlog.Core.ApplicationServices.People.Commands.EditPersonBirthDate;
using Zamin.MiniBlog.Core.ApplicationServices.People.Commands.EditPersonFirstName;
using Zamin.MiniBlog.Core.ApplicationServices.People.Commands.EditPersonLastName;
using Zamin.MiniBlog.Core.ApplicationServices.People.Commands.RemovePerson;
using Zamin.MiniBlog.Core.ApplicationServices.People.Queries.PeoplePaged;
using Zamin.MiniBlog.Core.ApplicationServices.People.Queries.PersonByBusinessId;
using Zamin.MiniBlog.Core.Domain.People.QueryResults;

namespace Zamin.MiniBlog.EndPoints.Api.People
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : BaseController
    {
        [HttpPost("Create")]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonCommand command)
            => await Create<CreatePersonCommand, Guid>(command);

        [HttpPut("EditFirstName")]
        public async Task<IActionResult> EditPersonFirstName([FromBody] EditPersonFirstNameCommand command)
            => await Edit(command);

        [HttpPut("EditLastName")]
        public async Task<IActionResult> EditPersonLastName([FromBody] EditPersonLastNameCommand command)
            => await Edit(command);

        [HttpPut("EditBirthDate")]
        public async Task<IActionResult> EditPersonBirthDate([FromBody] EditPersonBirthDateCommand command)
            => await Edit(command);

        [HttpDelete("Remove")]
        public async Task<IActionResult> RemovePerson([FromBody] RemovePersonCommand command)
            => await Delete(command);

        [HttpGet("GetByBusinessId")]
        public async Task<IActionResult> GetPersonByBusinessId([FromQuery] PersonByBusinessIdQuery query)
            => await Query<PersonByBusinessIdQuery, PersonQr>(query);

        [HttpGet("GetPaged")]
        public async Task<IActionResult> GetPersonsPaged([FromQuery] PeoplePagedQuery query)
            => await Query<PeoplePagedQuery, PagedData<PersonQr>>(query);
    }
}
