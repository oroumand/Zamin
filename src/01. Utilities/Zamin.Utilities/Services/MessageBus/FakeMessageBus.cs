using Microsoft.Extensions.Logging;

namespace Zamin.Utilities.Services.MessageBus
{
    public class FakeMessageBus : IMessageBus
    {
        private readonly ILogger<FakeMessageBus> _logger;

        public FakeMessageBus(ILogger<FakeMessageBus> logger)
        {
            _logger = logger;
        }
        public void Publish<TInput>(TInput input)
        {
            _logger.LogInformation("Message published by fake message bus: {input}", input.ToString());
        }

        public void Receive(string commandName)
        {
            _logger.LogInformation("fake message bus receive {commandName}", commandName);
        }

        public void Send(Parcel parcel)
        {
            _logger.LogInformation("Message send by fake message bus: {parcel}", parcel.ToString());
        }

        public void SendCommandTo<TCommandData>(string destinationService, string commandName, TCommandData commandData)
        {
            _logger.LogInformation("command send to {destinationService} by fake message bus. Command name: {commandName} and command data is {commandData}",
                destinationService, commandName, commandData.ToString());
        }

        public void SendCommandTo<TCommandData>(string destinationService, string commandName, string correlationId, TCommandData commandData)
        {
            _logger.LogInformation("command send to {destinationService} by fake message bus. Command name: {commandName} with correlation id: " +
                "{correlationId} and command data is {commandData}", destinationService, commandName, correlationId, commandData.ToString());
        }

        public void Subscribe(string serviceId, string eventName)
        {
            _logger.LogInformation("fake message bus subscribe for event: {eventName} from service {serviceId}", eventName, serviceId);
        }
    }
}
