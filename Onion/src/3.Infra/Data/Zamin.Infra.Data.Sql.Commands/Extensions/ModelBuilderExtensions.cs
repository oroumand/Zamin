using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Zamin.Core.Domain.Entities;
using Zamin.Core.Domain.ValueObjects;

namespace Zamin.Infra.Data.Sql.Commands.Extensions;
public static class ModelBuilderExtensions
{
	public static void AddBusinessId<TEntity, TId>(this ModelBuilder modelBuilder)
		where TEntity : AggregateRoot<TId>
	{
		foreach (var entityType in modelBuilder.Model.GetEntityTypes()
			.Where(e => typeof(TEntity).IsAssignableFrom(e.ClrType)))
		{
			modelBuilder.Entity<TEntity>()
				.Property<BusinessId>(nameof(AggregateRoot<TId>.BusinessId))
				.HasConversion(c => c.Value, d => BusinessId.FromGuid(d))
				.IsUnicode()
				.IsRequired();
			modelBuilder.Entity<TEntity>().HasAlternateKey(nameof(AggregateRoot<TId>.BusinessId));
		}
	}

	public static ModelBuilder UseValueConverterForType<T>(this ModelBuilder modelBuilder, ValueConverter<T, string> converter, int maxLength = 0)
	{
		return modelBuilder.UseValueConverterForType(typeof(T), converter, maxLength);
	}

	public static ModelBuilder UseValueConverterForType(this ModelBuilder modelBuilder, Type type, ValueConverter converter, int maxLength = 0)
	{
		foreach (var entityType in modelBuilder.Model.GetEntityTypes())
		{
			var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == type);

			foreach (var property in properties)
			{
				modelBuilder.Entity(entityType.ClrType).Property(property.Name)
					.HasConversion(converter);
				if (maxLength > 0)
					modelBuilder.Entity(entityType.ClrType).Property(property.Name).HasMaxLength(maxLength);
			}
		}

		return modelBuilder;
	}
}
