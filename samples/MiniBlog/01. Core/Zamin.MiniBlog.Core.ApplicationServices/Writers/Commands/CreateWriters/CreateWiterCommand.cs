using Zamin.Core.ApplicationServices.Commands;
using System;

namespace Zamin.MiniBlog.Core.ApplicationServices.Writers.Commands.CreatePerson
{
    public class CreateWiterCommand:ICommand<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid BusinessId { get; set; }
    }
}
