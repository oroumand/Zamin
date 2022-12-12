using Zamin.Extensions.Events.Outbox.Core.Model;
using Zamin.Extensions.Events.Outbox.Dal.EF.Configs;
using Zamin.Extensions.Events.Outbox.Dal.EF.Interceptors;
using Zamin.Infra.Data.Sql.Commands;

namespace Zamin.Extensions.Events.Outbox.Dal.EF;

public abstract class BaseOutboxCommandDbContext : BaseCommandDbContext
{
    public DbSet<OutBoxEventItem> OutBoxEventItems { get; set; }

    public BaseOutboxCommandDbContext(DbContextOptions options) : base(options)
    {

    }

    protected BaseOutboxCommandDbContext()
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors(new AddOutBoxEventItemInterceptor());
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new OutBoxEventItemConfig());
    }


}