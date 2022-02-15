using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Queries;

namespace MiniBlog.Infra.Data.Sql.Queries.Common
{
    public class MiniblogQueryDbContext : BaseQueryDbContext
    {
        public MiniblogQueryDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
