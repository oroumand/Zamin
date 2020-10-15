using System;

namespace Zamin.Core.Domain.ValueObjects
{
    /// <summary>
    /// کلاس پایه برای همه ValueObjectها
    /// توضحیات کاملی در مورد دلایل وجود ValueObjectها را در لینک زیر مشاهده می‌کند
    /// https://martinfowler.com/bliki/ValueObject.html
    /// </summary>
    /// <typeparam name="TValueObject"></typeparam>
    public abstract class BaseValueObject<TValueObject> : IEquatable<TValueObject>
            where TValueObject : BaseValueObject<TValueObject>
    {
        public bool Equals(TValueObject other)
        {
            return this == other;
        }
        public override bool Equals(object obj)
        {
            var otherObject = obj as TValueObject;
            if (otherObject == null)
                return false;
            return ObjectIsEqual(otherObject);
        }
        public override int GetHashCode()
        {
            return ObjectGetHashCode();
        }
        public abstract bool ObjectIsEqual(TValueObject otherObject);
        public abstract int ObjectGetHashCode();
        public static bool operator ==(BaseValueObject<TValueObject> right, BaseValueObject<TValueObject> left)
        {
            if (right is null && left is null)
                return true;
            if (right is null || left is null)
                return false;
            return right.Equals(left);
        }
        public static bool operator !=(BaseValueObject<TValueObject> right, BaseValueObject<TValueObject> left)
        {
            return !(right == left);
        }

    }
}
