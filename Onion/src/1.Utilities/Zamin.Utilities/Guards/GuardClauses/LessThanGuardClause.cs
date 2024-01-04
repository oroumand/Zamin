using Zamin.Core.Domain.Exceptions;

namespace SampleGuards.Guards.GuardClauses;

public static class LessThanGuardClause
{
    public static void LessThan<T>(this Guard guard, T value, T maximumValue, IComparer<T> comparer, string message, params string[] parameters)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        int comparerResult = comparer.Compare(value, maximumValue);

        if (comparerResult > -1)
            throw new InvalidEntityStateException(message, parameters);
    }

    public static void LessThan<T>(this Guard guard, T value, T maximumValue, string message, params string[] parameters)
        where T : IComparable<T>, IComparable
    {
        guard.LessThan(value, maximumValue, Comparer<T>.Default, message, parameters);
    }
}