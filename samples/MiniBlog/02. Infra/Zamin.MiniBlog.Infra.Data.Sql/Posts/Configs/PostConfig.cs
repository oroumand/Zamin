using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamin.MiniBlog.Core.Domain.Posts.Entities;
using Zamin.MiniBlog.Infra.Data.Sql.Commands.Common.ValueConverters;

namespace Zamin.MiniBlog.Infra.Data.Sql.Commands.Posts.Configs
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(c => c.PersonBusinessId)
                .HasConversion(new BusinessIdValueConverter()).IsRequired();

            builder.Property(c => c.Title).IsRequired();

            builder.Property(c => c.Content).IsRequired();
        }
    }
}
