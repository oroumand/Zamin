using Zamin.Core.Domain.Exceptions;

namespace SampleGuards.Guards.GuardClauses;

public static class NullGuardClause
{
    public static void Null<T>(this Guard guard, T value, string message, params string[] parameters)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (value != null)
            throw new InvalidEntityStateException(message, parameters);
    }
}
