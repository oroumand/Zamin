using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniBlog.Core.Domain.Blogs.Entities;
using Zamin.Infra.Data.Sql.Extentions;

namespace MiniBlog.Infra.Data.Sql.Commands.Blogs.Configs;

public class BlogConfig : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.AddDeletedShadowProperty().IgnoreDeletedQueryFilter();
    }
}
