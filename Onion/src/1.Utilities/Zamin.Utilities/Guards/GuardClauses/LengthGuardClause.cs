using System.Collections;

namespace Zamin.Utilities.Guards.GuardClauses;

public static class LengthGuardClause
{
    public static void Length(this Guard guard, string value, int length, string message)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (value.Length != length)
            throw new InvalidOperationException(message);
    }

    public static void Length(this Guard guard, ICollection value, int length, string message)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (value.Count != length)
            throw new InvalidOperationException(message);
    }
}