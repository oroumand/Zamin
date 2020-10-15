using Zamin.Core.Domain.Exceptions;
using System;

namespace Zamin.Core.Domain.ValueObjects
{
    public class BusinessId : BaseValueObject<BusinessId>
    {
        public static BusinessId FromString(string value) => new BusinessId(value);
        public static BusinessId FromGuid(Guid value) => new BusinessId { Value = value };
        public BusinessId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidValueObjectStateException("ValidationErrorIsRequire", nameof(BusinessId));
            }
            if (value.Length != 32)
            {
                throw new InvalidValueObjectStateException("ValidationErrorStringLength", nameof(BusinessId), "32", "32");
            }

            if (Guid.TryParse(value, out Guid tempValue))
            {
                Value = tempValue;
            }
            else
            {
                throw new InvalidValueObjectStateException("ValidationErrorInvalidValue", nameof(BusinessId));
            }
        }
        private BusinessId()
        {

        }

        public Guid Value { get; private set; }

        public override int ObjectGetHashCode()
        {

            return Value.GetHashCode();
        }

        public override bool ObjectIsEqual(BusinessId otherObject)
        {
            return Value == otherObject.Value;
        }
        public override string ToString()
        {
            return Value.ToString();
        }

        public static explicit operator string(BusinessId title) => title.Value.ToString();
        public static implicit operator BusinessId(string value) => new BusinessId(value);


        public static explicit operator Guid(BusinessId title) => title.Value;
        public static implicit operator BusinessId(Guid value) => new BusinessId { Value = value };

    }
}

