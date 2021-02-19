using Zamin.Core.ApplicationServices.Commands;
using Zamin.MiniBlog.Core.Domain.People.Entities;
using Zamin.MiniBlog.Core.Domain.People.Repositories;
using Zamin.Utilities;
using System.Threading.Tasks;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.CreatePerson
{
    public class CreatePersonCommandHandler : CommandHandler<CreatePersonCommand, long>
    {
        private readonly IPersonCommandRepository _personRepository;

        public CreatePersonCommandHandler(ZaminServices hamoonServices, IPersonCommandRepository personRepository) : base(hamoonServices)
        {
            _personRepository = personRepository;
        }

        public override Task<CommandResult<long>> Handle(CreatePersonCommand request)
        {
            Person person = new Person
            {                
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = null
            };
            _personRepository.Insert(person);
            _personRepository.Commit();            
            return OkAsync(person.Id);
        }
    }
}
