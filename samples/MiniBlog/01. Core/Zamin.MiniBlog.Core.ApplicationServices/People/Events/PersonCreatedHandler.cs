using Zamin.Core.ApplicationServices.Events;
using Zamin.MiniBlog.Core.Domain.People.Events;
using Zamin.MiniBlog.Core.Domain.People.Repositories;
using System;
using System.Threading.Tasks;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Events
{
    //public class PersonCreatedHandler : IDomainEventHandler<PersonCreated>
    //{
    //    public Task Handle(PersonCreated @event)
    //    {
    //        Console.WriteLine(
    //            $"Person : BusinessId = {0} , FirstName = {1} , LastName = {2} , BirthDate = {3}",
    //            @event.BusinessId,
    //            @event.FirstName,
    //            @event.LastName,
    //            @event.BirthDate);

    //        return Task.CompletedTask;
    //    }
    //}
}
