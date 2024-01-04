using Zamin.Core.Domain.Exceptions;

namespace SampleGuards.Guards.GuardClauses;

public static class EqualGuardClause
{
    public static void Equal<T>(this Guard guard, T value, T targetValue, string message, params string[] parameters)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (!Equals(value, targetValue))
            throw new InvalidEntityStateException(message, parameters);
    }

    public static void Equal<T>(this Guard guard, T value, T targetValue, IEqualityComparer<T> equalityComparer, string message, params string[] parameters)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (!equalityComparer.Equals(value, targetValue))
            throw new InvalidEntityStateException(message, parameters);
    }
}