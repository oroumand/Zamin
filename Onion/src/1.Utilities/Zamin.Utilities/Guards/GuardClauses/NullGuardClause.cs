namespace Zamin.Utilities.Guards.GuardClauses;

public static class NullGuardClause
{
    public static void Null<T>(this Guard guard, T value, string message)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (value != null)
            throw new InvalidOperationException(message);
    }
}
