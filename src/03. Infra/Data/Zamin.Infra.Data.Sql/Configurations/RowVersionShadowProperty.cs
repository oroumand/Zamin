using Zamin.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Zamin.Infra.Data.Sql.Configurations
{
    public static class RowVersionShadowProperty
    {
        public static readonly string RowVersion = nameof(RowVersion);

        public static void AddRowVersionShadowProperty(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model
                                       .GetEntityTypes()
                                       .Where(e => typeof(IRowVersion).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType).Property<byte[]>(RowVersion).IsRowVersion(); ;
            }
        }

    }
}
