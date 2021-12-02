using System;
using Zamin.Core.ApplicationServices.Commands;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.EditPersonBirthDate
{
    public class EditPersonBirthDateCommand : ICommand
    {
        public Guid BusinessId { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
