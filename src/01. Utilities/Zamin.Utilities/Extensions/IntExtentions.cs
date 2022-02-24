namespace Zamin.Utilities.Extensions
{
    public static class IntExtentions
    {
        public static bool IsEmpty(this int value) => value == default;

        public static bool IsNull(this int? value) => value == null;

        public static bool IsNullOrEmpty(this int? value) => value.IsNull() || value == default;

        public static bool IsGraterThan(this int value, int min) => value > min;

        public static bool IsGraterThan(this int? value, int min) => !value.IsNull() && value > min;

        public static bool IsGraterThanOrEqualTo(this int value, int min) => value >= min;

        public static bool IsGraterThanOrEqualTo(this int? value, int min) => !value.IsNull() && value >= min;

        public static bool IsLessThan(this int value, int max) => value < max;

        public static bool IsLessThan(this int? value, int max) => !value.IsNull() && value < max;

        public static bool IsLessThanOrEqualTo(this int value, int max) => value <= max;

        public static bool IsLessThanOrEqualTo(this int? value, int max) => !value.IsNull() && value <= max;

        public static bool IsValidRange(this int value, int min, int max) => value.IsGraterThanOrEqualTo(min) && value.IsLessThanOrEqualTo(max);

        public static bool IsValidRange(this int? value, int min, int max) => !value.IsNull() && value.IsGraterThanOrEqualTo(min) && value.IsLessThanOrEqualTo(max);
    }
}
