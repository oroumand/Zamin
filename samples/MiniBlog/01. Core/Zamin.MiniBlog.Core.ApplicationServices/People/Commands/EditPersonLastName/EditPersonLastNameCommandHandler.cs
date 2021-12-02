using System.Threading.Tasks;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.MiniBlog.Core.Domain.People.Repositories;
using Zamin.Utilities;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.EditPersonLastName
{
    public class EditPersonLastNameCommandHandler : CommandHandler<EditPersonLastNameCommand>
    {
        private readonly IPersonCommandRepository _commandRepository;

        public EditPersonLastNameCommandHandler(
            ZaminServices zaminServices,
            IPersonCommandRepository commandRepository) : base(zaminServices)
        {
            _commandRepository = commandRepository;
        }

        public override async Task<CommandResult> Handle(EditPersonLastNameCommand command)
        {
            var person = await _commandRepository.GetAsync(command.BusinessId);

            person.EditLastName(command.LastName);

            await _commandRepository.CommitAsync();

            return await OkAsync();
        }
    }
}
