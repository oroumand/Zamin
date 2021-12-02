using Zamin.Core.ApplicationServices.Commands;
using Zamin.MiniBlog.Core.Domain.People.Entities;
using Zamin.MiniBlog.Core.Domain.People.Repositories;
using Zamin.Utilities;
using System.Threading.Tasks;
using System;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.CreatePerson
{
    public class CreatePersonCommandHandler : CommandHandler<CreatePersonCommand, Guid>
    {
        private readonly IPersonCommandRepository _commandRepository;

        public CreatePersonCommandHandler(
            ZaminServices zaminServices,
            IPersonCommandRepository commandRepository) : base(zaminServices)
        {
            _commandRepository = commandRepository;
        }

        public override async Task<CommandResult<Guid>> Handle(CreatePersonCommand command)
        {
            var person = Person.Create(command.FirstName, command.LastName, command.BirthDate);

            await _commandRepository.InsertAsync(person);

            await _commandRepository.CommitAsync();

            return await OkAsync(person.BusinessId.Value);
        }
    }
}
