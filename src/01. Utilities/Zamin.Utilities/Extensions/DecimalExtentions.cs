namespace Zamin.Utilities.Extensions
{
    public static class DecimalExtentions
    {
        public static bool IsEmpty(this decimal value) => value == default;

        public static bool IsNull(this decimal? value) => value == null;

        public static bool IsNullOrEmpty(this decimal? value) => value.IsNull() || value == default;

        public static bool IsGraterThan(this decimal value, decimal min) => value > min;

        public static bool IsGraterThan(this decimal? value, decimal min) => !value.IsNull() && value > min;

        public static bool IsGraterThanOrEqualTo(this decimal value, decimal min) => value >= min;

        public static bool IsGraterThanOrEqualTo(this decimal? value, decimal min) => !value.IsNull() && value >= min;

        public static bool IsLessThan(this decimal value, decimal max) => value < max;

        public static bool IsLessThan(this decimal? value, decimal max) => !value.IsNull() && value < max;

        public static bool IsLessThanOrEqualTo(this decimal value, decimal max) => value <= max;

        public static bool IsLessThanOrEqualTo(this decimal? value, decimal max) => !value.IsNull() && value <= max;

        public static bool IsValidRange(this decimal value, decimal min, decimal max) => value.IsGraterThanOrEqualTo(min) && value.IsLessThanOrEqualTo(max);

        public static bool IsValidRange(this decimal? value, decimal min, decimal max) => !value.IsNull() && value.IsGraterThanOrEqualTo(min) && value.IsLessThanOrEqualTo(max);
    }
}
