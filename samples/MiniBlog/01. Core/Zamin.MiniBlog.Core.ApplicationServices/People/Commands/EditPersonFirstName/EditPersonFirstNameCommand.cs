using System;
using Zamin.Core.ApplicationServices.Commands;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.EditPersonFirstName
{
    public class EditPersonFirstNameCommand : ICommand
    {
        public Guid BusinessId { get; set; }
        public string FirstName { get; set; }
    }
}
