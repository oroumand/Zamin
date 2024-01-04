using Zamin.Core.Domain.Exceptions;

namespace SampleGuards.Guards.GuardClauses;

public static class NotEqualGuardClause
{
    public static void NotEqual<T>(this Guard guard, T value, T targetValue, string message, params string[] parameters)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (Equals(value, targetValue))
            throw new InvalidEntityStateException(message, parameters);
    }
}