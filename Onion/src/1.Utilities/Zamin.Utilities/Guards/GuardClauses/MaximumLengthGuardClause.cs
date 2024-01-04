using System.Collections;
using Zamin.Core.Domain.Exceptions;

namespace SampleGuards.Guards.GuardClauses;

public static class MaximumLengthGuardClause
{
    public static void MaximumLength(this Guard guard, string value, int maximumLength, string message, params string[] parameters)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (value.Length > maximumLength)
            throw new InvalidEntityStateException(message, parameters);
    }
    public static void MaximumLength(this Guard guard, ICollection value, int maximumLength, string message, params string[] parameters)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (value.Count > maximumLength)
            throw new InvalidEntityStateException(message, parameters);
    }
}
