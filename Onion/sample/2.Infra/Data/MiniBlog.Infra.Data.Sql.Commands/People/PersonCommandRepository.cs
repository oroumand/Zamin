using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.Domain.Blogs.Entities;
using MiniBlog.Infra.Data.Sql.Commands.Common;

using Zamin.Infra.Data.Sql.Commands;

namespace MiniBlog.Infra.Data.Sql.Commands.People;

public class PersonCommandRepository :
		BaseCommandRepository<Person, MiniblogCommandDbContext, long>,
		IPersonCommandRepository
{
	public PersonCommandRepository(MiniblogCommandDbContext dbContext) : base(dbContext)
	{
	}
}
