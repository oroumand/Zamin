using System;
using System.Text;

namespace Zamin.Utilities.Services.MessageBus
{
    public interface IMessageBus
    {
        void Publish<TInput>(TInput input);
        void SendCommandTo<TCommandData>(string destinationService, string commandName, TCommandData commandData);
        void SendCommandTo<TCommandData>(string destinationService, string commandName, string correlationId, TCommandData commandData);
        void Send(Parcel parcel);
        void Subscribe(string serviceId, string eventName);
        void Receive(string commandName);
    }
}
