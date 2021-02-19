using Zamin.Infra.Data.Sql.Queries;
using Microsoft.EntityFrameworkCore;

namespace Zamin.MiniBlog.Infra.Data.Sql.Queries.Common
{
    public class MiniblogQueryDbContext : BaseQueryDbContext
    {
        public MiniblogQueryDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
