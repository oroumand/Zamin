using Zamin.Core.ApplicationServices.Commands;
using Zamin.MiniBlog.Core.Domain.People.Repositories;
using Zamin.MiniBlog.Core.Domain.Writers.Repositories;
using Zamin.Utilities;
using System.Threading.Tasks;
using Zamin.Core.Contracts.ApplicationServices.Commands;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.TestCommands
{
    public class TestCommandHandler : CommandHandler<TestCommand>
    {
        public TestCommandHandler(ZaminServices zaminServices, IPersonCommandRepository personCommandRepository, IWriterQueryRepository writerQueryRepository) : base(zaminServices)
        {
        }

        public override Task<CommandResult> Handle(TestCommand request)
        {
            return OkAsync();
        }
    }
}
