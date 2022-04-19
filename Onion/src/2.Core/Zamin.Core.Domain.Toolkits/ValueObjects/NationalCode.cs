using Zamin.Core.Domain.Exceptions;
using Zamin.Core.Domain.ValueObjects;
using Zamin.Utilities.Extensions;

namespace Zamin.Core.Domain.Toolkits.ValueObjects;
public class NationalCode : BaseValueObject<NationalCode>
{
    #region Properties
    public string Value { get; private set; }
    #endregion

    #region Constructors and Factories
    public static NationalCode FromString(string value) => new(value);
    public NationalCode(string value)
    {
        if (!value.IsNationalCode())
        {
            throw new InvalidValueObjectStateException("ValidationErrorStringFormat", nameof(NationalCode));
        }

        Value = value;
    }
    private NationalCode()
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

    public static explicit operator string(NationalCode title) => title.Value;
    public static implicit operator NationalCode(string value) => new(value);
    #endregion

    #region Methods
    public override string ToString() => Value;

    #endregion

}

