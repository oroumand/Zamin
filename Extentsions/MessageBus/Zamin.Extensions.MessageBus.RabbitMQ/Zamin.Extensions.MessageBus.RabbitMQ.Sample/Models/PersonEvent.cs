namespace Zamin.Extensions.MessageBus.RabbitMQ.Sample.Models
{
    public class PersonEvent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class PersonCommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
