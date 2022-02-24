namespace Zamin.Utilities.Extensions
{
    public static class DateTimeExtentions
    {
        public static bool IsEmpty(this DateTime value) => value == default;

        public static bool IsNull(this DateTime? value) => value == null;

        public static bool IsNullOrEmpty(this DateTime? value) => value.IsNull() || value == default;

        public static bool IsGraterThan(this DateTime value, DateTime min) => value > min;

        public static bool IsGraterThan(this DateTime? value, DateTime min) => !value.IsNull() && value > min;

        public static bool IsGraterThanOrEqualTo(this DateTime value, DateTime min) => value >= min;

        public static bool IsGraterThanOrEqualTo(this DateTime? value, DateTime min) => !value.IsNull() && value >= min;

        public static bool IsLessThan(this DateTime value, DateTime max) => value < max;

        public static bool IsLessThan(this DateTime? value, DateTime max) => !value.IsNull() && value < max;

        public static bool IsLessThanOrEqualTo(this DateTime value, DateTime max) => value <= max;

        public static bool IsLessThanOrEqualTo(this DateTime? value, DateTime max) => !value.IsNull() && value <= max;

        public static bool IsValidRange(this DateTime value, DateTime min, DateTime max) => value.IsGraterThanOrEqualTo(min) && value.IsLessThanOrEqualTo(max);

        public static bool IsValidRange(this DateTime? value, DateTime min, DateTime max) => !value.IsNull() && value.IsGraterThanOrEqualTo(min) && value.IsLessThanOrEqualTo(max);
    }
}
