namespace Zamin.Utilities.Services.MessageBus;
public interface IReceiveMessageBus
{
    void Subscribe(string serviceId, string eventName);
    void Receive(string commandName);
}

