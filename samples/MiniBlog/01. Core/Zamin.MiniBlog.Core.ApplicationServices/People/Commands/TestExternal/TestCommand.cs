using Zamin.Core.Contracts.ApplicationServices.Commands;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.TestExternal
{
    public class TestCommand : ICommand
    {
        public string Name { get; set; }
    }
}
