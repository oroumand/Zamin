﻿using Zamin.Core.Domain.Entities;
using Zamin.Core.Domain.ValueObjects;
using Zamin.Utilities.Services.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Zamin.Infra.Data.Sql.Configurations
{
    public static class AuditableShadowProperties
    {
        public static readonly Func<object, int?> EFPropertyCreatedByUserId =
                                        entity => EF.Property<int?>(entity, CreatedByUserId);
        public static readonly string CreatedByUserId = nameof(CreatedByUserId);

        public static readonly Func<object, int?> EFPropertyModifiedByUserId =
                                        entity => EF.Property<int?>(entity, ModifiedByUserId);
        public static readonly string ModifiedByUserId = nameof(ModifiedByUserId);

        public static readonly Func<object, DateTime?> EFPropertyCreatedDateTime =
                                        entity => EF.Property<DateTime?>(entity, CreatedDateTime);
        public static readonly string CreatedDateTime = nameof(CreatedDateTime);

        public static readonly Func<object, DateTime?> EFPropertyModifiedDateTime =
                                        entity => EF.Property<DateTime?>(entity, ModifiedDateTime);
        public static readonly string ModifiedDateTime = nameof(ModifiedDateTime);

        public static void AddAuditableShadowProperties(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model
                                                   .GetEntityTypes()
                                                   .Where(e => typeof(IAuditable).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType)
                            .Property<int?>(CreatedByUserId);
                modelBuilder.Entity(entityType.ClrType)
                            .Property<int?>(ModifiedByUserId);

                modelBuilder.Entity(entityType.ClrType)
                            .Property<DateTime?>(CreatedDateTime);
                modelBuilder.Entity(entityType.ClrType)
                            .Property<DateTime?>(ModifiedDateTime);

            }


        }
        public static void AddBusinessId(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model
                                                   .GetEntityTypes()
                                                   .Where(e => typeof(AggregateRoot).IsAssignableFrom(e.ClrType) ||
                                                        typeof(Entity).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property<BusinessId>("BusinessId").HasConversion(c => c.Value, d => BusinessId.FromGuid(d))
                    .IsUnicode()
                    .IsRequired();
                modelBuilder.Entity(entityType.ClrType).HasAlternateKey("BusinessId");
            }
        }

        public static void SetHasNoKey(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.ClrType)
                            .HasNoKey();
            }
        }

        public static void SetAuditableEntityPropertyValues(
            this ChangeTracker changeTracker,
            IUserInfoService userInfoService)
        {

            var userAgent = userInfoService.GetUserAgent();
            var userIp = userInfoService.GetUserIp();
            var now = DateTime.UtcNow;
            var userId = userInfoService.UserId();

            var modifiedEntries = changeTracker.Entries<IAuditable>()
                                               .Where(x => x.State == EntityState.Modified);
            foreach (var modifiedEntry in modifiedEntries)
            {
                modifiedEntry.Property(ModifiedDateTime).CurrentValue = now;
                modifiedEntry.Property(ModifiedByUserId).CurrentValue = userId;
            }

            var addedEntries = changeTracker.Entries<IAuditable>()
                                            .Where(x => x.State == EntityState.Added);
            foreach (var addedEntry in addedEntries)
            {
                addedEntry.Property(CreatedDateTime).CurrentValue = now;
                addedEntry.Property(CreatedByUserId).CurrentValue = userId;
            }
        }

        public static List<AggregateRoot> GetChangedAggregates(this ChangeTracker changeTracker) =>
            changeTracker.Entries<AggregateRoot>()
                                     .Where(x => x.State == EntityState.Modified ||
                                            x.State == EntityState.Added ||
                                            x.State == EntityState.Deleted).Select(c => c.Entity).ToList();

        public static List<EntityEntry<IAuditable>> GetChangedAuditable(this ChangeTracker changeTracker) =>
            changeTracker.Entries<IAuditable>()
                .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added || x.State == EntityState.Deleted).ToList();

        public static List<AggregateRoot> GetAggregatesWithEvent(this ChangeTracker changeTracker) =>
            changeTracker.Entries<AggregateRoot>()
                                     .Where(x => x.State != EntityState.Detached).Select(c => c.Entity).Where(c => c.GetEvents().Any()).ToList();

        public static ModelBuilder UseValueConverterForType<T>(this ModelBuilder modelBuilder, ValueConverter converter, int maxLenght = 0)
        {
            return modelBuilder.UseValueConverterForType(typeof(T), converter, maxLenght);
        }

        public static ModelBuilder UseValueConverterForType(this ModelBuilder modelBuilder, Type type, ValueConverter converter, int maxLength = 0)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == type);

                foreach (var property in properties)
                {
                    modelBuilder.Entity(entityType.Name).Property(property.Name)
                        .HasConversion(converter);
                    if (maxLength > 0)
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasMaxLength(maxLength);

                }
            }

            return modelBuilder;
        }

    }
}
