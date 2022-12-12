namespace Zamin.Extensions.MessageBus.Abstractions;

public interface IReceiveMessageBus
{
    void Subscribe(string serviceId, string eventName);
    void Receive(string commandName);
}
