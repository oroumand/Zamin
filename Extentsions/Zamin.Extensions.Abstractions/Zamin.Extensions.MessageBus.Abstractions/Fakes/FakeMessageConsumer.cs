namespace Zamin.Extensions.MessageBus.Abstractions.Fakes;

public class FakeMessageConsumer : IMessageConsumer
{
    public async Task<bool> ConsumeCommand(string sender, Parcel parcel)
    {
        Consume("command", sender, parcel);
        return true;
    }



    public async Task<bool> ConsumeEvent(string sender, Parcel parcel)
    {
        Consume("event", sender, parcel);
        return true;
    }

    private static void Consume(string type, string sender, Parcel parcel)
    {
        Console.WriteLine($"Message {parcel.MessageName} of type {type} by Id {parcel.MessageId} from route {parcel.Route} from service {sender} Consumed");
    }
}
