namespace Zamin.Utilities.Extensions
{
    public static class ByteExtentions
    {
        public static bool IsEmpty(this byte value) => value == default;

        public static bool IsNull(this byte? value) => value == null;

        public static bool IsNullOrEmpty(this byte? value) => value.IsNull() || value == default;

        public static bool IsGraterThan(this byte value, byte min) => value > min;

        public static bool IsGraterThan(this byte? value, byte min) => !value.IsNull() && value > min;

        public static bool IsGraterThanOrEqualTo(this byte value, byte min) => value >= min;

        public static bool IsGraterThanOrEqualTo(this byte? value, byte min) => !value.IsNull() && value >= min;

        public static bool IsLessThan(this byte value, byte max) => value < max;

        public static bool IsLessThan(this byte? value, byte max) => !value.IsNull() && value < max;

        public static bool IsLessThanOrEqualTo(this byte value, byte max) => value <= max;

        public static bool IsLessThanOrEqualTo(this byte? value, byte max) => !value.IsNull() && value <= max;

        public static bool IsValidRange(this byte value, byte min, byte max) => value.IsGraterThanOrEqualTo(min) && value.IsLessThanOrEqualTo(max);

        public static bool IsValidRange(this byte? value, byte min, byte max) => !value.IsNull() && value.IsGraterThanOrEqualTo(min) && value.IsLessThanOrEqualTo(max);
    }
}
