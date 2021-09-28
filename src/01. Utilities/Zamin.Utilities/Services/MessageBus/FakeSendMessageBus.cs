using Microsoft.Extensions.Logging;

namespace Zamin.Utilities.Services.MessageBus
{
    public class FakeSendMessageBus : ISendMessageBus
    {
        private readonly ILogger<FakeSendMessageBus> _logger;

        public FakeSendMessageBus(ILogger<FakeSendMessageBus> logger)
        {
            _logger = logger;
        }
        public void Publish<TInput>(TInput input)
        {
            _logger.LogInformation("Message published by fake message bus: {input}", input.ToString());
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


    }
}
