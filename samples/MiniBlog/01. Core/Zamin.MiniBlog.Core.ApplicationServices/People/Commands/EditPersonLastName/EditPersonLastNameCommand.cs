using System;
using Zamin.Core.ApplicationServices.Commands;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.EditPersonLastName
{
    public class EditPersonLastNameCommand : ICommand
    {
        public Guid BusinessId { get; set; }
        public string LastName { get; set; }
    }
}
