using Zamin.Infra.Data.Sql.Queries;
using Microsoft.EntityFrameworkCore;
using Zamin.MiniBlog.Infra.Data.Sql.Queries.Common.Models;

namespace Zamin.MiniBlog.Infra.Data.Sql.Queries.Common
{
    public class MiniblogQueryDbContext : BaseQueryDbContext
    {
        public MiniblogQueryDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Writer> Writers { get; set; }
        public virtual DbSet<Person> People { get; set; }
    }
}
