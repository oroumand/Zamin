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
using Zamin.Infra.Data.ChangeInterceptors.EntityChageInterceptorItems;
using Zamin.Utilities.Configurations;

namespace Zamin.Infra.Data.Sql.Commands
{
    public abstract class BaseCommandDbContext : DbContext
    {
        private IDbContextTransaction _transaction;

        public DbSet<OutBoxEventItem> OutBoxEventItems { get; set; }
        private readonly List<EntityChageInterceptorItem> entityChageInterceptorItems = new List<EntityChageInterceptorItem>();

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
            var _hamoonConfigurations = this.GetService<ZaminConfigurations>();
            setShadowProperties();
            if(_hamoonConfigurations.ApplicationEvents.TransactionalEventsEnabled)
                addOutboxEvetItems();
            if(_hamoonConfigurations.EntityChangeInterception.Enabled)
                addEntityChangeInterceptorItems();
        }


        private void setShadowProperties()
        {
            var userInfoService = this.GetService<IUserInfoService>();
            ChangeTracker.SetAuditableEntityPropertyValues(userInfoService);
        }

        private void addOutboxEvetItems()
        {
            var changedAggregates = ChangeTracker.GetAggregatesWithEvent();
            var userInfoService = this.GetService<IUserInfoService>();
            var serializer = this.GetService<IJsonSerializer>();
            foreach (var aggregate in changedAggregates)
            {
                var events = aggregate.GetEvents();
                foreach (var @event in events)
                {
                    OutBoxEventItems.Add(new OutBoxEventItem
                    {
                        EventId = Guid.NewGuid(),
                        AccuredByUserId = userInfoService.UserId().ToString(),
                        AccuredOn = DateTime.Now,
                        AggregateId = aggregate.BusinessId.ToString(),
                        AggregateName = aggregate.GetType().Name,
                        AggregateTypeName = aggregate.GetType().FullName,
                        EventName = @event.GetType().Name,
                        EventTypeName = @event.GetType().FullName,
                        EventPayload = serializer.Serilize(@event),
                        IsProcessed = false
                    });
                }
            }
        }

        private void addEntityChangeInterceptorItems()
        {
            var changedEntities = ChangeTracker.GetChangedAuditable();
            var transactionId = Guid.NewGuid().ToString();
            var dateOfAccured = DateTime.Now;
            var propertyForReject = new List<string>
            {
                AuditableShadowProperties.CreatedByUserId,
                AuditableShadowProperties.CreatedDateTime,
                AuditableShadowProperties.ModifiedByUserId,
                AuditableShadowProperties.ModifiedDateTime
            };
            var userInfoService = this.GetService<IUserInfoService>();
            var repository = this.GetService<IEntityChageInterceptorItemRepository>();
            var entityChageInterceptorItems = new List<EntityChageInterceptorItem>();
            foreach (var entity in changedEntities)
            {
                var entityChageInterceptorItem = new EntityChageInterceptorItem
                {
                    Id = Guid.NewGuid(),
                    TransactionId = transactionId,
                    UserId = userInfoService.UserId().ToString(),
                    Ip = userInfoService.GetUserIp(),
                    EntityType = entity.Entity.GetType().FullName,
                    EntityId = entity.Property("BusinessId").CurrentValue.ToString(),
                    DateOfOccurrence = dateOfAccured,
                    ChangeType = entity.State.ToString(),
                    ContextName = GetType().FullName
                };

                foreach (var property in entity.Properties.Where(c => propertyForReject.All(d => d != c.Metadata.Name)))
                {
                    if (entity.State == EntityState.Added || property.IsModified)
                    {                        
                        entityChageInterceptorItem.PropertyChangeLogItems.Add(new PropertyChangeLogItem
                        {
                            ChageInterceptorItemId = entityChageInterceptorItem.Id,
                            PropertyName = property.Metadata.Name,
                            Value = property.CurrentValue?.ToString(),
                        });
                    }
                }
                entityChageInterceptorItems.Add(entityChageInterceptorItem);
            }
            repository.Save(entityChageInterceptorItems);

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
                        var inverseNavigation = navigation.Inverse;
                        if (inverseNavigation != null)
                            includedNavigations.Add(inverseNavigation);
                    }
                    stack.Push(entityNavigations.GetEnumerator());
                }
                while (stack.Count > 0 && !stack.Peek().MoveNext())
                    stack.Pop();
                if (stack.Count == 0) break;
                entityType = stack.Peek().Current.TargetEntityType;
            }
        }
    }
}
