using Zamin.Infra.Data.Sql.Commands;
using Zamin.MiniBlog.Core.Domain.Writers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zamin.MiniBlog.Infra.Data.Sql.Commands.Common
{
    public class MiniblogDbContext : BaseCommandDbContext
    {
        public DbSet<Writer> Writers{ get; set; }
        public MiniblogDbContext(DbContextOptions<MiniblogDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(builder);
        }
    }
    public class DbContextBuilder : IDesignTimeDbContextFactory<MiniblogDbContext>
    {
        public MiniblogDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MiniblogDbContext>();
            builder.UseSqlServer("Server =.\\SQL2019; Database=MiniBlogDb ;User Id =sa;Password=1qaz!QAZ; MultipleActiveResultSets=true");
            return new MiniblogDbContext(builder.Options);
        }
    }
}
