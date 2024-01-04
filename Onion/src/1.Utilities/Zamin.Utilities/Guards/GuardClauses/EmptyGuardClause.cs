using System.Collections;
using Zamin.Core.Domain.Exceptions;

namespace SampleGuards.Guards.GuardClauses;

public static class EmptyGuardClause
{
    public static void Empty<T>(this Guard guard, T value, string message, params string[] parameters)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (value == null)
            return;

        if (value is ICollection collectionValue && collectionValue.Count == 0)
            return;

        if (value is IEnumerable enumerableValue && !enumerableValue.GetEnumerator().MoveNext())
            return;

        if (value is string strValue && string.IsNullOrWhiteSpace(strValue))
            return;

        if (!Equals(value, default(T)))
            throw new InvalidEntityStateException(message, parameters);
    }

    public static void Empty<T>(this Guard guard, T value, IEqualityComparer<T> equalityComparer, string message, params string[] parameters)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (!equalityComparer.Equals(value, default))
            throw new InvalidEntityStateException(message, parameters);
    }
}