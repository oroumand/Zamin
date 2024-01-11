namespace Zamin.Utilities.Guards.GuardClauses;

public static class EqualGuardClause
{
    public static void Equal<T>(this Guard guard, T value, T targetValue, string message)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (!Equals(value, targetValue))
            throw new InvalidOperationException(message);
    }

    public static void Equal<T>(this Guard guard, T value, T targetValue, IEqualityComparer<T> equalityComparer, string message)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (!equalityComparer.Equals(value, targetValue))
            throw new InvalidOperationException(message);
    }
}