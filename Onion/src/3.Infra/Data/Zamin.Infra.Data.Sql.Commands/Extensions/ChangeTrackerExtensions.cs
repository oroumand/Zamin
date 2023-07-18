using Microsoft.EntityFrameworkCore.ChangeTracking;

using Zamin.Core.Domain.Entities;

namespace Zamin.Infra.Data.Sql.Commands.Extensions;
public static class ChangeTrackerExtensions
{
	public static List<AggregateRoot<TId>> GetChangedAggregates<TId>(this ChangeTracker changeTracker) =>
		changeTracker.Aggregates<TId>().Where(IsModified<TId>()).Select(c => c.Entity).ToList();

	public static List<AggregateRoot<TId>> GetAggregatesWithEvent<TId>(this ChangeTracker changeTracker) =>
		changeTracker.Aggregates<TId>()
			.Where(IsNotDetached<TId>())
			.Select(c => c.Entity)
			.Where(c => c.GetEvents().Any())
			.ToList();

	public static IEnumerable<EntityEntry<AggregateRoot<TId>>> Aggregates<TId>(this ChangeTracker changeTracker) =>
		changeTracker.Entries<AggregateRoot<TId>>();

	private static Func<EntityEntry<AggregateRoot<TId>>, bool> IsNotDetached<TId>() =>
		x => x.State != EntityState.Detached;

	private static Func<EntityEntry<AggregateRoot<TId>>, bool> IsModified<TId>()
	{
		return x => x.State == EntityState.Modified ||
					x.State == EntityState.Added ||
					x.State == EntityState.Deleted;
	}
}
