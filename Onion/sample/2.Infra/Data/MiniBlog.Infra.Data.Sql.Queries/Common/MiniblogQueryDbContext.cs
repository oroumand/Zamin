using Microsoft.EntityFrameworkCore;
using MiniBlog.Core.RequestResponse.Blogs.Queries.GetById;
using MiniBlog.Infra.Data.Sql.Queries.Blogs;
using MiniBlog.Infra.Data.Sql.Queries.Common.LanguageService;
using Zamin.Infra.Data.Sql.Queries;

namespace MiniBlog.Infra.Data.Sql.Queries.Common
{
    public partial class MiniblogQueryDbContext : BaseQueryDbContext
    {
        private readonly ILanguageService _userService;
        public MiniblogQueryDbContext(DbContextOptions<MiniblogQueryDbContext> options, ILanguageService userService)
            : base(options)
        {
            this._userService = userService;
        }

        public virtual DbSet<Blog> Blogs { get; set; } = null!;
        public virtual DbSet<OutBoxEventItem> OutBoxEventItems { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server =.; Database = MiniBlogDb; User Id =sa; Password= 1qaz!QAZ; MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.Property(e => e.CreatedByUserId).HasMaxLength(50);

                entity.Property(e => e.ModifiedByUserId).HasMaxLength(50);
            });

            modelBuilder.Entity<Blog>().Property(e => e.Title)
            .HasConversion(c => c, c => TranslateData.Getvalue(c, _userService.GetLanguage()));

            modelBuilder.Entity<Blog>().Property(e => e.Description)
           .HasConversion(c => c, c => TranslateData.Getvalue(c, _userService.GetLanguage()));

            modelBuilder.Entity<OutBoxEventItem>(entity =>
            {
                entity.Property(e => e.AccuredByUserId).HasMaxLength(255);

                entity.Property(e => e.AggregateName).HasMaxLength(255);

                entity.Property(e => e.AggregateTypeName).HasMaxLength(500);

                entity.Property(e => e.EventName).HasMaxLength(255);

                entity.Property(e => e.EventTypeName).HasMaxLength(500);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
