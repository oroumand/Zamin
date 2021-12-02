using System;
using Zamin.Core.ApplicationServices.Commands;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.RemovePerson
{
    public class RemovePersonCommand : ICommand
    {
        public Guid BusinessId { get; set; }
    }
}
