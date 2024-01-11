namespace Zamin.Utilities.Guards.GuardClauses;

public static class InclusiveBetweenGuardClause
{
    public static void InclusiveBetween<T>(this Guard guard, T value, T minimumValue, T maximumValue, IComparer<T> comparer, string message)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        int minimumValueComparerResult = comparer.Compare(value, minimumValue);
        int maximumValueComparerResult = comparer.Compare(value, maximumValue);

        if (minimumValueComparerResult == -1)
            throw new InvalidOperationException(message);

        if (maximumValueComparerResult == 1)
            throw new InvalidOperationException(message);
    }

    public static void InclusiveBetween<T>(this Guard guard, T value, T minimumValue, T maximumValue, string message)
        where T : IComparable<T>, IComparable
    {
        guard.InclusiveBetween(value, minimumValue, maximumValue, Comparer<T>.Default, message);
    }
}