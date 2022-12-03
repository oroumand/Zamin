namespace Zamin.Extentions.MessageBus.Abstractions;

public interface IMessageConsumer
{
    bool ConsumeEvent(string sender, Parcel parcel);

    bool ConsumeCommand(string sender, Parcel parcel);
}