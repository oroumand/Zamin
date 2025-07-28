namespace Zamin.Utilities.Extensions;

public static class StringExtensions
{
    public const char ArabicYeChar = (char)1610;
    public const char PersianYeChar = (char)1740;

    public const char ArabicKeChar = (char)1603;
    public const char PersianKeChar = (char)1705;




    public static string ApplyCorrectYeKe(this object data)
    {
        return data == null ? null : ApplyCorrectYeKe(data.ToString());
    }

    public static string ApplyCorrectYeKe(this string data)
    {
        return string.IsNullOrWhiteSpace(data) ?
                    string.Empty :
                    data.Replace(ArabicYeChar, PersianYeChar).Replace(ArabicKeChar, PersianKeChar).Trim()
                        .Fa2En();
    }


    public static long ToSafeLong(this string input, long replacement = long.MinValue) =>
         long.TryParse(input, out long result) ? result : replacement;
    public static long? ToSafeNullableLong(this string input) =>
        long.TryParse(input, out long result) ? result : null;


    public static int ToSafeInt(this string input, int replacement = int.MinValue) =>
     int.TryParse(input, out int result) ? result : replacement;
    public static int? ToSafeNullableInt(this string input) =>
        int.TryParse(input, out int result) ? result : null;

    public static string ToStringOrEmpty(this string? input) => input ?? string.Empty;

    public static string ToUnderscoreCase(this string input) =>
        string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();

    public static byte[] ToByteArray(this string input)
    {
        return System.Text.Encoding.UTF8.GetBytes(input);
    }

    public static string FromByteArray(this byte[] input)
    {
        return System.Text.Encoding.UTF8.GetString(input);
    }
    public static string ToNumeric(this int value)
    {
        return value.ToString("N0"); //"123,456"
    }
    public static string ToCurrency(this int value)
    {
        //fa-IR => current culture currency symbol => ریال
        //123456 => "123,123ریال"
        return value.ToString("C0");
    }

    public static string En2Fa(this string str)
    {
        return str.Replace("0", "۰")
            .Replace("1", "۱")
            .Replace("2", "۲")
            .Replace("3", "۳")
            .Replace("4", "۴")
            .Replace("5", "۵")
            .Replace("6", "۶")
            .Replace("7", "۷")
            .Replace("8", "۸")
            .Replace("9", "۹");
    }

    public static string Fa2En(this string str)
    {
        return str.Replace("۰", "0")
            .Replace("۱", "1")
            .Replace("۲", "2")
            .Replace("۳", "3")
            .Replace("۴", "4")
            .Replace("۵", "5")
            .Replace("۶", "6")
            .Replace("۷", "7")
            .Replace("۸", "8")
            .Replace("۹", "9")
            //iphone numeric
            .Replace("٠", "0")
            .Replace("١", "1")
            .Replace("٢", "2")
            .Replace("٣", "3")
            .Replace("٤", "4")
            .Replace("٥", "5")
            .Replace("٦", "6")
            .Replace("٧", "7")
            .Replace("٨", "8")
            .Replace("٩", "9");
    }

}
