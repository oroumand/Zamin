using Zamin.MiniBlog.Core.Domain.People.Events;
using System;
using System.Threading.Tasks;
using Zamin.Core.Contracts.ApplicationServices.Events;

namespace Zamin.MiniBlog.Core.ApplicationServices.Writers.Events
{
    public class PersonCreatedHandler : IDomainEventHandler<PersonCreated>
    {
        public Task Handle(PersonCreated Event)
        {
            Console.WriteLine(Event.FirstName);
            return Task.CompletedTask;
        }
    }

    public class PersonUpdatedHandler : IDomainEventHandler<PersonUpdated>
    {
        public Task Handle(PersonUpdated Event)
        {
            Console.WriteLine(Event.FirstName);
            return Task.CompletedTask;
        }
    }
}
