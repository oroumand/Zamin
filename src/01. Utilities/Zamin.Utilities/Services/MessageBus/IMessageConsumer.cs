using System;

namespace Zamin.Utilities.Services.MessageBus
{
    public interface IMessageConsumer
    {
        void ConsumeEvent(string sender,Parcel parcel);
        void ConsumeCommand(string sender, Parcel parcel);
    }
    public class FakeMessageConsumer : IMessageConsumer
    {
        public void ConsumeCommand(string sender, Parcel parcel)
        {
            Consume("command", sender, parcel);
        }



        public void ConsumeEvent(string sender, Parcel parcel)
        {
            Consume("event", sender,parcel);
        }

        private static void Consume(string type, string sender, Parcel parcel)
        {
            Console.WriteLine($"Message {parcel.MessageName} of type {type} by Id {parcel.MessageId} from route {parcel.Route} from service {sender} Consumed");
        }
    }
}
