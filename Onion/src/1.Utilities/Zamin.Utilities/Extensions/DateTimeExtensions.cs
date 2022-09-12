using System.Globalization;

namespace Zamin.Utilities.Extensions;

public static class DateTimeExtensions
{
    public static long ToUnixTimeMillisecond(this DateTime dateTime, TimeZoneInfo timeZoneInfo)
        => TimeZoneInfo.ConvertTime(new DateTimeOffset(dateTime), timeZoneInfo).ToUnixTimeMilliseconds();

    public static long? ToUnixTimeMillisecond(this DateTime? dateTime, TimeZoneInfo timeZoneInfo)
        => dateTime is not null ? TimeZoneInfo.ConvertTime(new DateTimeOffset(dateTime.Value), timeZoneInfo).ToUnixTimeMilliseconds() : null;

    public static DateTime ToDateTime(this long unixTimeMilliseconds, TimeZoneInfo timeZoneInfo)
        => TimeZoneInfo.ConvertTime(DateTimeOffset.FromUnixTimeMilliseconds(unixTimeMilliseconds), timeZoneInfo).DateTime;

    public static DateTime? ToDateTime(this long? unixTimeMilliseconds, TimeZoneInfo timeZoneInfo)
        => unixTimeMilliseconds is not null ? TimeZoneInfo.ConvertTime(DateTimeOffset.FromUnixTimeMilliseconds(unixTimeMilliseconds.Value), timeZoneInfo).DateTime : null;

    public static long ToLocalUnixTimeMillisecond(this DateTime dateTime) => dateTime.ToUnixTimeMillisecond(TimeZoneInfo.Local);

    public static long? ToLocalUnixTimeMillisecond(this DateTime? dateTime) => dateTime.ToUnixTimeMillisecond(TimeZoneInfo.Local);

    public static DateTime ToLocalDateTime(this long unixTimeMilliseconds) => unixTimeMilliseconds.ToDateTime(TimeZoneInfo.Local);

    public static DateTime? ToLocalDateTime(this long? unixTimeMilliseconds) => unixTimeMilliseconds.ToDateTime(TimeZoneInfo.Local);

    public static long ToUtcUnixTimeMillisecond(this DateTime dateTime) => dateTime.ToUnixTimeMillisecond(TimeZoneInfo.Utc);

    public static long? ToUtcUnixTimeMillisecond(this DateTime? dateTime) => dateTime.ToUnixTimeMillisecond(TimeZoneInfo.Utc);

    public static DateTime ToUtcDateTime(this long unixTimeMilliseconds) => unixTimeMilliseconds.ToDateTime(TimeZoneInfo.Utc);

    public static DateTime? ToUtcDateTime(this long? unixTimeMilliseconds) => unixTimeMilliseconds.ToDateTime(TimeZoneInfo.Utc);

    public static string ToShamsiDateStirng(this DateTime date)
    {
        PersianCalendar persianCalendar = new();
        return $"{persianCalendar.GetYear(date)}/{persianCalendar.GetMonth(date)}/{persianCalendar.GetDayOfMonth(date)}";
    }
}