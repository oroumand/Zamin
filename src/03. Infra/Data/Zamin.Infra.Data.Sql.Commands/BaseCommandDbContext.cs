using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Zamin.Infra.Data.Sql.Configurations;
using Zamin.Infra.Data.Sql.Commands.OutBoxEventItems;
using Zamin.Infra.Events.Outbox;
using Zamin.Utilities.Services.Users;
using Zamin.Utilities.Services.Serializers;

namespace Zamin.Infra.Data.Sql.Commands
{
    public abstract class BaseCommandDbContext : DbContext
    {
        private IDbContextTransaction _transaction;

        public DbSet<OutBoxEventItem> OutBoxEventItems { get; set; }

        public BaseCommandDbContext(DbContextOptions options) : base(options)
        {

        }

        protected BaseCommandDbContext()
        {
        }

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public void RollbackTransaction()
        {
            if (_transaction == null)
            {
                throw new NullReferenceException("Please call `BeginTransaction()` method first.");
            }
            _transaction.Rollback();
        }

        public void CommitTransaction()
        {
            if (_transaction == null)
            {
                throw new NullReferenceException("Please call `BeginTransaction()` method first.");
            }
            _transaction.Commit();
        }

        public T GetShadowPropertyValue<T>(object entity, string propertyName) where T : IConvertible
        {
            var value = Entry(entity).Property(propertyName).CurrentValue;
            return value != null
                ? (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture)
                : default;
        }

        public object GetShadowPropertyValue(object entity, string propertyName)
        {
            return Entry(entity).Property(propertyName).CurrentValue;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.AddAuditableShadowProperties();
            builder.AddBusinessId();
            builder.ApplyConfiguration(new OutBoxEventItemConfig());
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            beforeSaveTriggers();
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChanges();
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();
            beforeSaveTriggers();
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        private void beforeSaveTriggers()
        {
            setShadowProperties();
            addOutboxEvetItems();
        }

        private void addOutboxEvetItems()
        {
            var changedAggregates = ChangeTracker.GetChangedAggregates();
            var userInfoService = this.GetService<IUserInfoService>();
            var serializer = this.GetService<IJsonSerializer>();
            foreach (var aggregate in changedAggregates)
            {
                var events = aggregate.GetEvents();
                foreach (var @event in events)
                {
                    OutBoxEventItems.Add(new OutBoxEventItem
                    {
                        AccuredByUserId = userInfoService.UserId().ToString(),
                        AggregateId = aggregate.BusinessId.ToString(),
                        AggregateName = aggregate.GetType().Name,
                        AggregateTypeName = aggregate.GetType().FullName,
                        EventDate = DateTime.Now,
                        EventName = @event.GetType().Name,
                        EventTypeName = aggregate.GetType().Name,
                        EventPayload = serializer.Serilize(@event)
                    });
                }
            }
        }

        private void setShadowProperties()
        {
            var userInfoService = this.GetService<IUserInfoService>();
            ChangeTracker.SetAuditableEntityPropertyValues(userInfoService);
        }

        public IEnumerable<string> GetIncludePaths(Type clrEntityType)
        {
            var entityType = Model.FindEntityType(clrEntityType);
            var includedNavigations = new HashSet<INavigation>();
            var stack = new Stack<IEnumerator<INavigation>>();
            while (true)
            {
                var entityNavigations = new List<INavigation>();
                foreach (var navigation in entityType.GetNavigations())
                {
                    if (includedNavigations.Add(navigation))
                        entityNavigations.Add(navigation);
                }
                if (entityNavigations.Count == 0)
                {
                    if (stack.Count > 0)
                        yield return string.Join(".", stack.Reverse().Select(e => e.Current.Name));
                }
                else
                {
                    foreach (var navigation in entityNavigations)
                    {
                        var inverseNavigation = navigation.FindInverse();
                        if (inverseNavigation != null)
                            includedNavigations.Add(inverseNavigation);
                    }
                    stack.Push(entityNavigations.GetEnumerator());
                }
                while (stack.Count > 0 && !stack.Peek().MoveNext())
                    stack.Pop();
                if (stack.Count == 0) break;
                entityType = stack.Peek().Current.GetTargetType();
            }
        }
    }
}
