using System;
using Zamin.Core.ApplicationServices.Commands;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.CreatePerson
{
    public class CreatePersonCommand : ICommand<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
