using System.Threading.Tasks;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Data;
using Zamin.MiniBlog.Core.Domain.People.Repositories;
using Zamin.MiniBlog.Core.Domain.People.Services.RemovePerson;
using Zamin.MiniBlog.Core.Domain.Posts.Repositories;
using Zamin.Utilities;

namespace Zamin.MiniBlog.Core.ApplicationServices.People.Commands.RemovePerson
{
    public class RemovePersonCommandHandler : CommandHandler<RemovePersonCommand>
    {
        private readonly IPersonCommandRepository _personCommandRepository;
        private readonly IPostCommandRepository _postCommandRepository;
        private readonly IRemovePersonDomainService _removePersonDomainService;
        private readonly IUnitOfWork _unitOfWork;

        public RemovePersonCommandHandler(
            ZaminServices zaminServices,
            IPersonCommandRepository personCommandRepository,
            IPostCommandRepository postCommandRepository,
            IRemovePersonDomainService removePersonDomainService,
            IUnitOfWork unitOfWork) : base(zaminServices)
        {
            _personCommandRepository = personCommandRepository;
            _postCommandRepository = postCommandRepository;
            _removePersonDomainService = removePersonDomainService;
            _unitOfWork = unitOfWork;
        }

        public override async Task<CommandResult> Handle(RemovePersonCommand command)
        {
            await _removePersonDomainService.Execute(_personCommandRepository, _postCommandRepository, command.BusinessId);

            await _unitOfWork.CommitAsync();

            return await OkAsync();
        }
    }
}
