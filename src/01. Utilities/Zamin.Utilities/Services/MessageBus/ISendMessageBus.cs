namespace Zamin.Utilities.Services.MessageBus
{
    public interface ISendMessageBus
    {
        void Publish<TInput>(TInput input);
        void SendCommandTo<TCommandData>(string destinationService, string commandName, TCommandData commandData);
        void SendCommandTo<TCommandData>(string destinationService, string commandName, string correlationId, TCommandData commandData);
        void Send(Parcel parcel);
    }
}
