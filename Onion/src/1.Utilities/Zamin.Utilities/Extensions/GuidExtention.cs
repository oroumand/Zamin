namespace Zamin.Utilities.Extensions;

public static class GuidExtention
{
    public static bool IsNullOrEmpty(this Guid? guid) => guid == null || guid == default;

    public static bool IsEmpty(this Guid guid) => guid == default;
}