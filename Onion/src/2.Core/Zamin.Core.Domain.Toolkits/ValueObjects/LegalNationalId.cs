using Zamin.Core.Domain.Exceptions;
using Zamin.Core.Domain.ValueObjects;
using Zamin.Utilities.Extensions;

namespace Zamin.Core.Domain.Toolkits.ValueObjects;
public class LegalNationalId : BaseValueObject<LegalNationalId>
{
    #region Properties
    public string Value { get; private set; }
    #endregion

    #region Constructors and Factories
    public static LegalNationalId FromString(string value) => new(value);
    private LegalNationalId(string value)
    {
        if (!value.IsLegalNationalIdValid())
        {
            throw new InvalidValueObjectStateException("ValidationErrorStringFormat", nameof(LegalNationalId));
        }

        Value = value;
    }
    private LegalNationalId()
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
    public static explicit operator string(LegalNationalId title) => title.Value;
    public static implicit operator LegalNationalId(string value) => new(value);
    #endregion
    #region Methods
    public override string ToString() => Value;

    #endregion

}

