using MiniBlog.Core.Contracts.People;
using MiniBlog.Core.Domain.People.Entities;
using MiniBlog.Infra.Data.Sql.Commands.Common;
using Zamin.Infra.Data.Sql.Commands;

namespace MiniBlog.Infra.Data.Sql.Commands.People;

public class PersonCommandRepository :
        BaseCommandRepository<Person, MiniblogCommandDbContext, int>,
        IPersonCommandRepository
{
    public PersonCommandRepository(MiniblogCommandDbContext dbContext) : base(dbContext)
    {
    }
}
