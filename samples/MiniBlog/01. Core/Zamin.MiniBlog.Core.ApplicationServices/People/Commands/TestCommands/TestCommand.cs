using Zamin.Core.Contracts.ApplicationServices.Commands;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.TestCommands
{
    public class TestCommand : ICommand
    {
        public string Name { get; set; }
    }
}
