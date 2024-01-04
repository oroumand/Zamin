using Zamin.Core.Domain.Exceptions;

namespace SampleGuards.Guards.GuardClauses;

public static class InclusiveBetweenGuardClause
{
    public static void InclusiveBetween<T>(this Guard guard, T value, T minimumValue, T maximumValue, IComparer<T> comparer, string message, params string[] parameters)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        int minimumValueComparerResult = comparer.Compare(value, minimumValue);
        int maximumValueComparerResult = comparer.Compare(value, maximumValue);

        if (minimumValueComparerResult == -1)
            throw new InvalidEntityStateException(message, parameters);

        if (maximumValueComparerResult == 1)
            throw new InvalidEntityStateException(message, parameters);
    }

    public static void InclusiveBetween<T>(this Guard guard, T value, T minimumValue, T maximumValue, string message, params string[] parameters)
        where T : IComparable<T>, IComparable
    {
        guard.InclusiveBetween(value, minimumValue, maximumValue, Comparer<T>.Default, message, parameters);
    }
}