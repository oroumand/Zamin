using Zamin.Core.Domain.ValueObjects;

namespace Zamin.Core.Domain.Entities;
/// <summary>
/// کلاس پایه برای تمامی Entityها موجود در سامانه
/// </summary>

public abstract class Entity<TId>: IAuditableEntity
          where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
{
    /// <summary>
    /// شناسه عددی Entityها
    /// صرفا برای ذخیره در دیتابیس و سادگی کار مورد استفاده قرار بگیرید.
    /// </summary>
    public TId Id { get; protected set; }

    /// <summary>
    /// شناسه Entity
    /// شناسه اصلی Entity که در همه جا باید مورد استفاده قرار گیرد BusinessId است.
    /// تمامی ارتباطات به کمک این شناسه باید برقرار شود.
    /// </summary>
    public BusinessId BusinessId { get; protected set; } = BusinessId.FromGuid(Guid.NewGuid());

    /// <summary>
    /// سازنده پیش‌فرض به صورت Protected تعریف شده است.
    /// با توجه به اینکه این نیاز است هنگام ساخت خواص اساسی Entity ایجاد شود، هیچ شی بدون پر کردن این خواص نباید ایجاد شود.
    /// بار جلو گیری از این مورد برای همه Entityها باید سازنده‌هایی تعریف شود که مقدار ورودی دارند.
    /// برای اینکه بتوان از همین Entityها برای فرایند ذخیره سازی و بازیابی از دیتابیس به کمک ORMها استفاده کرد، ضروری است که سازنده پیش‌فرض با سطح دسترسی بالا مثل Protected یا Private ایجاد شود.
    /// </summary>
    protected Entity() { }


    #region Equality Check
    public bool Equals(Entity<TId>? other) => this == other;
    public override bool Equals(object? obj)=>
         obj is Entity<TId> otherObject && Id.Equals(otherObject.Id);

    public override int GetHashCode() => Id.GetHashCode();
    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
        => !(right == left);

    #endregion
}


public abstract class Entity : Entity<long>
{

}
