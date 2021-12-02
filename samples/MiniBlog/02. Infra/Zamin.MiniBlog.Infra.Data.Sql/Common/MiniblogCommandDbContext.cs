using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zamin.Infra.Data.Sql.Commands;
using Zamin.MiniBlog.Core.Domain.People.Entities;
using Zamin.MiniBlog.Core.Domain.Posts.Entities;

namespace Zamin.MiniBlog.Infra.Data.Sql.Commands.Common
{
    public class MiniblogCommandDbContext : BaseCommandDbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Post> Posts { get; set; }

        public MiniblogCommandDbContext(DbContextOptions<MiniblogCommandDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
