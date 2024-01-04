using System.Collections;
using Zamin.Core.Domain.Exceptions;

namespace SampleGuards.Guards.GuardClauses;

public static class MinimumLengthGuardClause
{
    public static void MinimumLength(this Guard guard, string value, int minimumLength, string message, params string[] parameters)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (value.Length < minimumLength)
            throw new InvalidEntityStateException(message, parameters);
    }
    public static void MinimumLength<T>(this Guard guard, ICollection value, int minimumLength, string message, params string[] parameters)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (value.Count < minimumLength)
            throw new InvalidEntityStateException(message, parameters);
    }
}
