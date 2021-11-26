using Zamin.MiniBlog.Core.ApplicationServices.People.Commands.CreatePerson;
using Zamin.MiniBlog.Core.ApplicationServices.People.Commands.TestExternal;
using Zamin.Utilities.Services.MessageBus;

namespace Zamin.MiniBlog.EndPoints.Api.Controllers;

[Route("api/[Controller]")]
public class PeopleController : BaseController
{
    private readonly ISendMessageBus _messageBus;

    public PeopleController(ISendMessageBus messageBus)
    {
        _messageBus = messageBus;
    }
    [HttpPost("TestEvent")]
    public async Task<IActionResult> TestEvent([FromBody] CreatePersonCommand createPerson)
    {

        return await Create<CreatePersonCommand, long>(createPerson);
    }

    [HttpGet("TestCommand")]
    public IActionResult TestCommand([FromQuery] string name)
    {
        _messageBus.SendCommandTo("MiniBlogService01", "TestCommand", new TestCommand
        {
            Name = name
        });

        return Ok("Command Sent");
    }
}