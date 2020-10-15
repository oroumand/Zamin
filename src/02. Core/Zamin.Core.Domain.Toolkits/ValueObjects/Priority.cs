using Zamin.Core.Domain.Exceptions;
using Zamin.Core.Domain.ValueObjects;

namespace Zamin.Core.Domain.Toolkits.ValueObjects
{
    public class Priority : BaseValueObject<Priority>
    {
        public static Priority FromInt(int value) => new Priority(value);
        public Priority(int value)
        {
            if (value < 1)
            {
                throw new InvalidValueObjectStateException("ValidationErrorValueGraterThan", nameof(Priority), "0");
            }
            Value = value;
        }
        private Priority()
        {

        }

        public int Value { get; private set; }

        public override int ObjectGetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool ObjectIsEqual(Priority otherObject)
        {
            return Value == otherObject.Value;
        }
        public static Priority operator +(Priority priority, int addNum)
        {
            return new Priority(priority.Value + addNum);
        }
        public static Priority operator -(Priority priority, int addNum)
        {
            return new Priority(priority.Value - addNum);
        }

        public static bool operator <(Priority priority01, Priority priority02)
        {
            return priority01.Value < priority02.Value;
        }

        public static bool operator >(Priority priority01, Priority priority02)
        {
            return priority01.Value > priority02.Value;
        }

        public static bool operator <=(Priority priority01, Priority priority02)
        {
            return priority01.Value <= priority02.Value;
        }

        public static bool operator >=(Priority priority01, Priority priority02)
        {
            return priority01.Value >= priority02.Value;
        }

        public static explicit operator int(Priority priority) => priority.Value;
        public static implicit operator Priority(int value) => new Priority(value);

        public Priority Increase(int increasedValue)
        {
            return new Priority(Value + increasedValue);
        }

        public Priority Decrease(int increasedValue)
        {
            return new Priority(Value - increasedValue);
        }
    }
}
