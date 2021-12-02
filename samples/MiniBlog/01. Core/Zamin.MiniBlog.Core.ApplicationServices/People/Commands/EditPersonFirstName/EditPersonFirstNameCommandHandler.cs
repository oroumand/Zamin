using System.Threading.Tasks;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.MiniBlog.Core.Domain.People.Repositories;
using Zamin.Utilities;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.EditPersonFirstName
{
    public class EditPersonFirstNameCommandHandler : CommandHandler<EditPersonFirstNameCommand>
    {
        private readonly IPersonCommandRepository _commandRepository;

        public EditPersonFirstNameCommandHandler(
            ZaminServices zaminServices,
            IPersonCommandRepository commandRepository) : base(zaminServices)
        {
            _commandRepository = commandRepository;
        }

        public override async Task<CommandResult> Handle(EditPersonFirstNameCommand command)
        {
            var person = await _commandRepository.GetAsync(command.BusinessId);

            person.EditFirstName(command.FirstName);

            await _commandRepository.CommitAsync();

            return await OkAsync();
        }
    }
}
