using Zamin.Core.Domain.Exceptions;

namespace Zamin.Utilities.Extensions;

public static class ObjectExtensions
{
    public static void AllowUseInstance(this object obj , string message)
    {
        if (obj is null)
            throw new InvalidEntityStateException(message);
    }
}