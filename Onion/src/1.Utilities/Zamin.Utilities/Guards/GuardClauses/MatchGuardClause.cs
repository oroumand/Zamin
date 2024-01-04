using System.Text.RegularExpressions;
using Zamin.Core.Domain.Exceptions;

namespace SampleGuards.Guards.GuardClauses;

public static class MatchGuardClause
{
    public static void Match(this Guard guard, string value, string pattern, string message, params string[] parameters)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (!Regex.IsMatch(value, pattern))
            throw new InvalidEntityStateException(message, parameters);
    }
}