namespace Zamin.Utilities.Extensions
{
    public static class GuidExtentions
    {
        public static bool IsEmpty(this Guid value) => value == default;

        public static bool IsNull(this Guid? value) => value == null;

        public static bool IsNullOrEmpty(this Guid? value) => value.IsNull() || value == default;
    }
}
