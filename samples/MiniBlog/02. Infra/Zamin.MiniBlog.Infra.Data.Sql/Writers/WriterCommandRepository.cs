using Zamin.Infra.Data.Sql.Commands;
using Zamin.MiniBlog.Core.Domain.Writers.Entities;
using Zamin.MiniBlog.Core.Domain.Writers.Repositories;
using Zamin.MiniBlog.Infra.Data.Sql.Commands.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zamin.MiniBlog.Infra.Data.Sql.Commands.Writers
{
    public class WriterCommandRepository : BaseCommandRepository<Writer, MiniblogDbContext>, IWriterRepository
    {
        public WriterCommandRepository(MiniblogDbContext dbContext) : base(dbContext)
        {
        }
    }
}
