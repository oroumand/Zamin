using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Exceptions;
using Zamin.MiniBlog.Core.Domain.People.Entities;
using Zamin.MiniBlog.Core.Domain.People.Repositories;
using Zamin.Utilities;
using System.Threading.Tasks;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.CreatePerson
{
    public class CreatePersonCommandHandler : CommandHandler<CreatePersonCommand, long>
    {
        private readonly IPersonCommandRepository _personRepository;

        public CreatePersonCommandHandler(ZaminServices zaminServices, IPersonCommandRepository personRepository) : base(zaminServices)
        {
            _personRepository = personRepository;

        }

        public override  Task<CommandResult<long>> Handle(CreatePersonCommand request)
        {
           // throw new InvalidEntityStateException("test");
            Person person = new Person(request.FirstName, request.LastName, null);
            _personRepository.Insert(person);
             _personRepository.Commit();
            return OkAsync(person.Id);
        }
    }
}
