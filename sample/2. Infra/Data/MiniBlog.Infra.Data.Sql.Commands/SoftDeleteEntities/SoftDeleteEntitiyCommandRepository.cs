using MiniBlog.Core.Contracts.SoftDeleteEntities.Repositories;
using MiniBlog.Core.Domain.SoftDeleteEntities.Entities;
using MiniBlog.Infra.Data.Sql.Commands.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamin.Infra.Data.Sql.Commands;

namespace MiniBlog.Infra.Data.Sql.Commands.SoftDeleteEntities
{
    public class SoftDeleteEntitiyCommandRepository :
        BaseCommandRepository<SoftDeleteEntity, MiniblogCommandDbContext>, ISoftDeleteEntitiyCommandRepository
    {
        public SoftDeleteEntitiyCommandRepository(MiniblogCommandDbContext dbContext) : base(dbContext)
        {
        }
    }
}
