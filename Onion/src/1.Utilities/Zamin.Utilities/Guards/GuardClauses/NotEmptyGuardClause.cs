using System.Collections;

namespace Zamin.Utilities.Guards.GuardClauses;

public static class NotEmptyGuardClause
{
    public static void NotEmpty<T>(this Guard guard, T value, string message)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (value == null)
            throw new InvalidOperationException(message);

        if (value is ICollection collectionValue && collectionValue.Count == 0)
            throw new InvalidOperationException(message);

        if (value is IEnumerable enumerableValue && !enumerableValue.GetEnumerator().MoveNext())
            throw new InvalidOperationException(message);

        if (value is string strValue && string.IsNullOrWhiteSpace(strValue))
            throw new InvalidOperationException(message);

        if (EqualityComparer<T>.Default.Equals(value, default))
            throw new InvalidOperationException(message);
    }

    public static void NotEmpty<T>(this Guard guard, T value, IEqualityComparer<T> equalityComparer, string message)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (equalityComparer.Equals(value, default))
            throw new InvalidOperationException(message);
    }
}