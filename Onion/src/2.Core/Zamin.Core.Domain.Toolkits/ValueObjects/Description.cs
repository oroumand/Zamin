using Zamin.Core.Domain.Exceptions;
using Zamin.Core.Domain.ValueObjects;

namespace Zamin.Core.Domain.Toolkits.ValueObjects;

public class Description : BaseValueObject<Description>
{
    #region Properties
    public string Value { get; private set; }
    #endregion

    #region Constructors and Factories
    public static Description FromString(string value) => new(value);
    public Description(string value)
    {
        if (!string.IsNullOrWhiteSpace(value) && value.Length > 500)
        {
            throw new InvalidValueObjectStateException("ValidationErrorIsRequire", nameof(Description), "0", "500");
        }

        Value = value;
    }
    private Description()
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
    public static explicit operator string(Description description) => description.Value;

    public static implicit operator Description(string value) => new(value);
    #endregion
}

