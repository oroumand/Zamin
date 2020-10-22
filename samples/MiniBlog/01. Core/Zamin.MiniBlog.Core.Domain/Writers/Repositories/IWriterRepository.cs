using Zamin.Core.Domain.Data;
using Zamin.MiniBlog.Core.Domain.Writers.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zamin.MiniBlog.Core.Domain.Writers.Repositories
{
    public interface IWriterRepository:ICommandRepository<Writer>
    {
    }
}
