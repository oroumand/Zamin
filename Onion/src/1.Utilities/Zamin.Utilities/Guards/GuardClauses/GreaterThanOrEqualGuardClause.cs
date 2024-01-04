using Zamin.Core.Domain.Exceptions;

namespace SampleGuards.Guards.GuardClauses;

public static class GreaterThanOrEqualGuardClause
{
    public static void GreaterThanOrEqual<T>(this Guard guard, T value, T minimumValue, IComparer<T> comparer, string message, params string[] parameters)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        int comparerResult = comparer.Compare(value, minimumValue);

        if (comparerResult < 0)
            throw new InvalidEntityStateException(message, parameters);
    }

    public static void GreaterThanOrEqual<T>(this Guard guard, T value, T minimumValue, string message, params string[] parameters)
        where T : IComparable<T>, IComparable
    {
        guard.GreaterThanOrEqual(value, minimumValue, Comparer<T>.Default, message, parameters);
    }
}