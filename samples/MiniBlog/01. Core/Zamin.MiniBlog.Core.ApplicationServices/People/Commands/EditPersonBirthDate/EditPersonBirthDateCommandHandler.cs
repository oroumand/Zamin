using System.Threading.Tasks;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.MiniBlog.Core.Domain.People.Repositories;
using Zamin.Utilities;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.EditPersonBirthDate
{
    public class EditPersonBirthDateCommandHandler : CommandHandler<EditPersonBirthDateCommand>
    {
        private readonly IPersonCommandRepository _commandRepository;

        public EditPersonBirthDateCommandHandler(
            ZaminServices zaminServices,
            IPersonCommandRepository commandRepository) : base(zaminServices)
        {
            _commandRepository = commandRepository;
        }

        public override async Task<CommandResult> Handle(EditPersonBirthDateCommand command)
        {
            var person = await _commandRepository.GetAsync(command.BusinessId);

            person.EditBirthDate(command.BirthDate);

            await _commandRepository.CommitAsync();

            return await OkAsync();
        }
    }
}
