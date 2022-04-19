using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System.Globalization;
using Zamin.Infra.Data.Sql.Commands.OutBoxEventItems;
using Zamin.Infra.Data.Sql.Extensions;
using Zamin.Extentions.UsersManagement.Abstractions;

namespace Zamin.Infra.Data.Sql.Commands;
public abstract class BaseCommandDbContext : DbContext
{
    protected IDbContextTransaction _transaction;

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
        BeforeSaveTriggers();
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
        BeforeSaveTriggers();
        ChangeTracker.AutoDetectChangesEnabled = false;
        var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        ChangeTracker.AutoDetectChangesEnabled = true;
        return result;
    }

    protected virtual void BeforeSaveTriggers()
    {
        SetShadowProperties();
    }

    private void SetShadowProperties()
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