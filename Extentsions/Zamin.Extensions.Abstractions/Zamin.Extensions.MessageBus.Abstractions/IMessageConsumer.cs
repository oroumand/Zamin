namespace Zamin.Extensions.MessageBus.Abstractions;

public interface IMessageConsumer
{
    Task<bool> ConsumeEvent(string sender, Parcel parcel);
    Task<bool> ConsumeCommand(string sender, Parcel parcel);
}
