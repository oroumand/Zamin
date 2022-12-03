namespace Zamin.Extentions.MessageBus.Abstractions;

public interface IMessageConsumer
{
    Task<bool> ConsumeEventAsync(string sender, Parcel parcel);

    Task<bool> ConsumeCommandAsync(string sender, Parcel parcel);
}