using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniBlog.Core.Domain.Blogs.Entities;

namespace MiniBlog.Infra.Data.Sql.Commands.Blogs.Configs;

public sealed class BlogConfig : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.HasMany(c => c.Posts).WithOne()
             .HasPrincipalKey(c => c.Id).HasForeignKey(c => c.BlogId);
    }
}