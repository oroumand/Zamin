using System.Collections;
using Zamin.Core.Domain.Exceptions;

namespace SampleGuards.Guards.GuardClauses;

public static class NotEmptyGuardClause
{
    public static void NotEmpty<T>(this Guard guard, T value, string message, params string[] parameters)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (value == null)
            throw new InvalidEntityStateException(message, parameters);

        if (value is ICollection collectionValue && collectionValue.Count == 0)
            throw new InvalidEntityStateException(message, parameters);

        if (value is IEnumerable enumerableValue && !enumerableValue.GetEnumerator().MoveNext())
            throw new InvalidEntityStateException(message, parameters);

        if (value is string strValue && string.IsNullOrWhiteSpace(strValue))
            throw new InvalidEntityStateException(message, parameters);

        if (EqualityComparer<T>.Default.Equals(value, default))
            throw new InvalidEntityStateException(message, parameters);
    }

    public static void NotEmpty<T>(this Guard guard, T value, IEqualityComparer<T> equalityComparer, string message, params string[] parameters)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (equalityComparer.Equals(value, default))
            throw new InvalidEntityStateException(message, parameters);
    }
}