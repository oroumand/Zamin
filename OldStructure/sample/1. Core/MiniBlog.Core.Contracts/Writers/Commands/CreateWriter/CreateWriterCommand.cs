using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlog.Core.Contracts.Writers.Commands.CreateWriter;
public class CreateWriterCommand
{
    public Guid BusunessId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

}
