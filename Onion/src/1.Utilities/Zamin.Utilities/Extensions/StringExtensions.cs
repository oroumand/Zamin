namespace Zamin.Utilities.Extensions;

public static class StringExtensions
{
    public static long ToSafeLong(this string input, long replacement = long.MinValue) =>
         long.TryParse(input, out long result) ? result : replacement;
    public static long? ToSafeNullableLong(this string input) =>
        long.TryParse(input, out long result) ? result : null;


    public static int ToSafeInt(this string input, int replacement = int.MinValue) =>
     int.TryParse(input, out int result) ? result : replacement;
    public static int? ToSafeNullableInt(this string input) =>
        int.TryParse(input, out int result) ? result : null;

    public static string ToStringOrEmpty(this string? input)=> input ?? string.Empty;

    public static string ToUnderscoreCase(this string input)=>
        string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();


}
