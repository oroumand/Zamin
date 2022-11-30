using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.Domain.Blogs.Entities;
using MiniBlog.Infra.Data.Sql.Commands.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamin.Infra.Data.Sql.Commands;

namespace MiniBlog.Infra.Data.Sql.Commands.People;

public class PersonCommandRepository :
        BaseCommandRepository<Person, MiniblogCommandDbContext>,
        IPersonCommandRepository
{
    public PersonCommandRepository(MiniblogCommandDbContext dbContext) : base(dbContext)
    {
    }
}
