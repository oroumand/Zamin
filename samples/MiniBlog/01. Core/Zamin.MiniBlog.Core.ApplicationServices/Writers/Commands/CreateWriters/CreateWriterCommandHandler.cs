using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.ValueObjects;
using Zamin.MiniBlog.Core.Domain.Writers.Entities;
using Zamin.MiniBlog.Core.Domain.Writers.Repositories;
using Zamin.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Zamin.MiniBlog.Core.ApplicationServices.Writers.Commands.CreatePerson
{
    public class CreateWriterCommandHandler : CommandHandler<CreateWiterCommand, long>
    {
        private readonly IWriterRepository _writerRepository;

        public CreateWriterCommandHandler(ZaminServices zaminServices, IWriterRepository writerRepository) : base(zaminServices)
        {
            this._writerRepository = writerRepository;
        }

        public override Task<CommandResult<long>> Handle(CreateWiterCommand request)
        {
            Writer writer = new Writer(BusinessId.FromGuid(request.BusinessId), request.FirstName, request.LastName);
            _writerRepository.Insert(writer);
            _writerRepository.Commit();
            return OkAsync(writer.Id);
        }
    }
}
