using Zamin.EndPoints.Web.Controllers;
using Zamin.MiniBlog.Core.ApplicationServices.People.Commands.CreatePerson;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Zamin.MiniBlog.EndPoints.Api.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[Controller]")]
    public class PeopleController:BaseController
    {
        [HttpPost("/Save")]
        public async Task<IActionResult> Post([FromBody]CreatePersonCommand createPerson)
        {
            return await Create<CreatePersonCommand, long>(createPerson);
        }
    }
}
