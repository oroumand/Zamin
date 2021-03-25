using Zamin.EndPoints.Web.Controllers;
using Zamin.MiniBlog.Core.ApplicationServices.People.Commands.CreatePerson;
using Zamin.MiniBlog.Core.ApplicationServices.People.Commands.TestExternal;
using Zamin.Utilities.Services.MessageBus;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Zamin.MiniBlog.EndPoints.Api.Controllers
{
    [Route("api/[Controller]")]
    public class PeopleController : BaseController
    {
        private readonly IMessageBus _messageBus;

        //private readonly MiniblogDbContext _miniblogDbContext;

        //public PeopleController(MiniblogDbContext miniblogDbContext)
        //{
        //    _miniblogDbContext = miniblogDbContext;
        //}
        public PeopleController(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }
        [HttpPost("/Save")]
        public async Task<IActionResult> Post([FromBody] CreatePersonCommand createPerson)
        {

            return await Create<CreatePersonCommand, long>(createPerson);
        }

        [HttpPost("/TestExternal")]
        public IActionResult TestExternal([FromBody] string name)
        {
            _messageBus.SendCommandTo("MiniBlogService01", "TestCommand", new TestCommand
            {
                Name = name
            });

            return Ok("Command Sent");
        }
    }
}
