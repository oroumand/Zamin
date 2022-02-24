namespace Zamin.Utilities.Extensions
{
    public static class ShortExtentions
    {
        public static bool IsEmpty(this short value) => value == default;

        public static bool IsNull(this short? value) => value == null;

        public static bool IsNullOrEmpty(this short? value) => value.IsNull() || value == default;

        public static bool IsGraterThan(this short value, short min) => value > min;

        public static bool IsGraterThan(this short? value, short min) => !value.IsNull() && value > min;

        public static bool IsGraterThanOrEqualTo(this short value, short min) => value >= min;

        public static bool IsGraterThanOrEqualTo(this short? value, short min) => !value.IsNull() && value >= min;

        public static bool IsLessThan(this short value, short max) => value < max;

        public static bool IsLessThan(this short? value, short max) => !value.IsNull() && value < max;

        public static bool IsLessThanOrEqualTo(this short value, short max) => value <= max;

        public static bool IsLessThanOrEqualTo(this short? value, short max) => !value.IsNull() && value <= max;

        public static bool IsValidRange(this short value, short min, short max) => value.IsGraterThanOrEqualTo(min) && value.IsLessThanOrEqualTo(max);

        public static bool IsValidRange(this short? value, short min, short max) => !value.IsNull() && value.IsGraterThanOrEqualTo(min) && value.IsLessThanOrEqualTo(max);
    }
}
