namespace Zamin.Utilities.Guards.GuardClauses;

public static class GreaterThanGuardClause
{
    public static void GreaterThan<T>(this Guard guard, T value, T minimumValue, IComparer<T> comparer, string message)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        int maximumValueComparerResult = comparer.Compare(value, minimumValue);

        if (maximumValueComparerResult != 1)
            throw new InvalidOperationException(message);
    }

    public static void GreaterThan<T>(this Guard guard, T value, T minimumValue, string message)
        where T : IComparable<T>, IComparable
    {
        guard.GreaterThan(value, minimumValue, Comparer<T>.Default, message);
    }
}