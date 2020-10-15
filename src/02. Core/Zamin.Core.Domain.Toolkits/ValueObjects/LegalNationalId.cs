using Zamin.Core.Domain.Exceptions;
using Zamin.Core.Domain.ValueObjects;
using Zamin.Utilities.Extentions;

namespace Zamin.Core.Domain.Toolkits.ValueObjects
{
    public class LegalNationalId : BaseValueObject<LegalNationalId>
    {
        public static LegalNationalId FromString(string value) => new LegalNationalId(value);
        public LegalNationalId(string value)
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

        public string Value { get; private set; }

        public override int ObjectGetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool ObjectIsEqual(LegalNationalId otherObject)
        {
            return Value == otherObject.Value;
        }
        public override string ToString()
        {
            return Value;
        }

        public static explicit operator string(LegalNationalId title) => title.Value;
        public static implicit operator LegalNationalId(string value) => new LegalNationalId(value);

    }
}
