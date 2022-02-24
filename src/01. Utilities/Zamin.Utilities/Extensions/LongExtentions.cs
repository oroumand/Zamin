namespace Zamin.Utilities.Extensions
{
    public static class LongExtentions
    {
        public static bool IsEmpty(this long value) => value == default;

        public static bool IsNull(this long? value) => value == null;

        public static bool IsNullOrEmpty(this long? value) => value.IsNull() || value == default;

        public static bool IsGraterThan(this long value, long min) => value > min;

        public static bool IsGraterThan(this long? value, long min) => !value.IsNull() && value > min;

        public static bool IsGraterThanOrEqualTo(this long value, long min) => value >= min;

        public static bool IsGraterThanOrEqualTo(this long? value, long min) => !value.IsNull() && value >= min;

        public static bool IsLessThan(this long value, long max) => value < max;

        public static bool IsLessThan(this long? value, long max) => !value.IsNull() && value < max;

        public static bool IsLessThanOrEqualTo(this long value, long max) => value <= max;

        public static bool IsLessThanOrEqualTo(this long? value, long max) => !value.IsNull() && value <= max;

        public static bool IsValidRange(this long value, long min, long max) => value.IsGraterThanOrEqualTo(min) && value.IsLessThanOrEqualTo(max);

        public static bool IsValidRange(this long? value, long min, long max) => !value.IsNull() && value.IsGraterThanOrEqualTo(min) && value.IsLessThanOrEqualTo(max);
    }
}
