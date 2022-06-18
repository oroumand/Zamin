using Microsoft.EntityFrameworkCore;
using MiniBlog.Core.Domain.Blogs.Entities;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Infra.Data.Sql.Commands;

namespace MiniBlog.Infra.Data.Sql.Commands.Common
{
    public class MiniblogCommandDbContext : BaseOutboxCommandDbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public MiniblogCommandDbContext(DbContextOptions<MiniblogCommandDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }
}
