﻿using Zamin.Infra.Data.Sql.Commands;
using Zamin.MiniBlog.Core.Domain.People.Entities;
using Zamin.MiniBlog.Core.Domain.People.Repositories;
using Zamin.MiniBlog.Infra.Data.Sql.Commands.Common;

namespace Zamin.MiniBlog.Infra.Data.Sql.Commands.People
{
    public class PersonCommandRepository : BaseCommandRepository<Person, MiniblogCommandDbContext>, IPersonCommandRepository
    {
        public PersonCommandRepository(MiniblogCommandDbContext dbContext) : base(dbContext)
        {
        }
    }
}
