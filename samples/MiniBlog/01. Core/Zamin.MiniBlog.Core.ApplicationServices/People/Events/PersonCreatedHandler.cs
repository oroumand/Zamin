using Zamin.MiniBlog.Core.Domain.People.Events;
using Zamin.MiniBlog.Core.Domain.People.Repositories;
using System;
using System.Threading.Tasks;
using Zamin.Core.Contracts.ApplicationServices.Events;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Events
{
    public class PersonCreatedHandler : IDomainEventHandler<PersonCreated>
    {
        private readonly IPersonCommandRepository personCommandRepository;

        public PersonCreatedHandler(IPersonCommandRepository personCommandRepository)
        {
            this.personCommandRepository = personCommandRepository;
        }
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
