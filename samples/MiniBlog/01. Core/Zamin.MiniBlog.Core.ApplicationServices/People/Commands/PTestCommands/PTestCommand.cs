using Zamin.Core.Contracts.ApplicationServices.Commands;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.PTestCommands
{
    public class PTestCommand : ICommand
    {
        public string Name { get; set; }
    }
}
