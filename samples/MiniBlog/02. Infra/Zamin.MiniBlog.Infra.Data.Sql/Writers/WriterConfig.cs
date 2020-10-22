using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.MiniBlog.Core.Domain.Writers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zamin.MiniBlog.Infra.Data.Sql.Commands.Writers
{
    public class WriterConfig : IEntityTypeConfiguration<Writer>
    {
        public void Configure(EntityTypeBuilder<Writer> builder)
        {
            builder.Property(c => c.FirstName).HasConversion(c => c.Value, c => Title.FromString(c));
            builder.Property(c => c.LastName).HasConversion(c => c.Value, c => Title.FromString(c));
        }
    }
}
