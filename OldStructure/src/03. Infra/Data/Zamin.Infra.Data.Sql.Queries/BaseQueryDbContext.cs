using Microsoft.EntityFrameworkCore;

namespace Zamin.Infra.Data.Sql.Queries;
public abstract class BaseQueryDbContext : DbContext
    {
        public BaseQueryDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public sealed override int SaveChanges()
        {
            throw new NotSupportedException();
        }
        public sealed override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            throw new NotSupportedException();

        }
        public sealed override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException();

        }
        public sealed override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException();

        }
    }

