using Microsoft.AspNetCore.Mvc;
using Zamin.Extensions.MessageBus.RabbitMQ.Sample.Models;
using Zamin.Extensions.MessageBus.Abstractions;

namespace Zamin.Extensions.MessageBus.RabbitMQ.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventAndCommandSampleController : ControllerBase
    {
        private readonly ISendMessageBus _sendMessageBus;

        public EventAndCommandSampleController(ISendMessageBus sendMessageBus)
        {
            _sendMessageBus = sendMessageBus;
        }
        [HttpPost("SendEvent")]
        public IActionResult SendEvent([FromBody] PersonEvent personEvent)
        {
            _sendMessageBus.Publish(personEvent);
            return Ok();
        }

        [HttpPost("SendCommand")]
        public IActionResult SendCommand([FromBody] PersonCommand PersonCommand)
        {
            _sendMessageBus.SendCommandTo("SampleApplciatoin", "PersonCommand", PersonCommand);
            return Ok();
        }
    }
}
