using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Queries;

namespace Zamin.MiniBlog.Infra.Data.Sql.Queries.Common
{
    public partial class MiniblogQueryDbContext : BaseQueryDbContext
    {
        public MiniblogQueryDbContext(DbContextOptions<MiniblogQueryDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<OutBoxEventItem> OutBoxEventItems { get; set; }
        public virtual DbSet<ParrotTranslation> ParrotTranslations { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server =.; Database=MiniBlogDb ; Integrated Security = True; MultipleActiveResultSets = true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OutBoxEventItem>(entity =>
            {
                entity.Property(e => e.AccuredByUserId).HasMaxLength(255);

                entity.Property(e => e.AggregateName).HasMaxLength(255);

                entity.Property(e => e.AggregateTypeName).HasMaxLength(500);

                entity.Property(e => e.EventName).HasMaxLength(255);

                entity.Property(e => e.EventTypeName).HasMaxLength(500);
            });

            modelBuilder.Entity<ParrotTranslation>(entity =>
            {
                entity.Property(e => e.Culture).HasMaxLength(5);

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasIndex(e => e.BusinessId, "AK_People_BusinessId")
                    .IsUnique();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasIndex(e => e.BusinessId, "AK_Posts_BusinessId")
                    .IsUnique();

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Title).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
