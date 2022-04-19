namespace Zamin.Core.Domain.Exceptions;
public class InvalidEntityStateException : DomainStateException
{
    /// <summary>
    /// خطا‌های مربوط به وضعیت ناصحیح در Entity ها توسط این کلاس ارسال می‌شود
    /// </summary>
    /// <param name="message">پیام یا الگوی پیام خطا</param>
    /// <param name="parameters">پارامتر‌ها که در صورت وجود در الگوی پیام جایگذاری می‌شوند</param>
    public InvalidEntityStateException(string message, params string[] parameters) : base(message)
    {
        Parameters = parameters;
    }
}
