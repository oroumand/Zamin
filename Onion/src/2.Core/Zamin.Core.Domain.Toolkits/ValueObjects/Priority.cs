using Zamin.Core.Domain.Exceptions;
using Zamin.Core.Domain.ValueObjects;

namespace Zamin.Core.Domain.Toolkits.ValueObjects;
public class Priority : BaseValueObject<Priority>
{
    #region Properties
    public int Value { get; private set; }
    #endregion

    #region Constructors and Factories
    public static Priority FromInt(int value) => new(value);
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
    #endregion

    #region Equality Check
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    #endregion

    #region Operator Overloading
    public static Priority operator +(Priority priority, int addNum) => new(priority.Value + addNum);

    public static Priority operator -(Priority priority, int addNum) => new(priority.Value - addNum);

    public static bool operator <(Priority priority01, Priority priority02) => priority01.Value < priority02.Value;

    public static bool operator >(Priority priority01, Priority priority02) => priority01.Value > priority02.Value;

    public static bool operator <=(Priority priority01, Priority priority02) => priority01.Value <= priority02.Value;

    public static bool operator >=(Priority priority01, Priority priority02) => priority01.Value >= priority02.Value;


    public static explicit operator int(Priority priority) => priority.Value;

    public static implicit operator Priority(int value) => new(value);

    #endregion

    #region Methods
    public Priority Increase(int increasedValue) => new(Value + increasedValue);

    public Priority Decrease(int increasedValue) => new(Value - increasedValue);
    #endregion

}

