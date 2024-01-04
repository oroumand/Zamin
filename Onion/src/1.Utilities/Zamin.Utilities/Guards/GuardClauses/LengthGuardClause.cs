using System.Collections;
using Zamin.Core.Domain.Exceptions;

namespace SampleGuards.Guards.GuardClauses;

public static class LengthGuardClause
{
    public static void Length(this Guard guard, string value, int length, string message, params string[] parameters)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (value.Length != length)
            throw new InvalidEntityStateException(message, parameters);
    }

    public static void Length(this Guard guard, ICollection value, int length, string message, params string[] parameters)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (value.Count != length)
            throw new InvalidEntityStateException(message, parameters);
    }
}