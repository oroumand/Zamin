using Zamin.MiniBlog.Core.Domain.People.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamin.MiniBlog.Infra.Data.Sql.Commands.Common.ValueConverters;

namespace Zamin.MiniBlog.Infra.Data.Sql.Commands.People.Configs
{
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(c => c.FirstName)
                .HasConversion(new TitleValueConverter()).HasMaxLength(50).IsRequired();

            builder.Property(c => c.LastName)
                .HasConversion(new TitleValueConverter()).HasMaxLength(50).IsRequired();
        }
    }
}