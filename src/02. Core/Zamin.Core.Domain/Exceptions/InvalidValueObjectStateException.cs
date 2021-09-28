namespace Zamin.Core.Domain.Exceptions
{
    /// <summary>
    /// خطا‌های مربوط به وضعیت ناصحیح در ValueObject ها توسط این کلاس ارسال می‌شود
    /// </summary>
    /// <param name="message">پیام یا الگوی پیام خطا</param>
    /// <param name="parameters">پارامتر‌ها که در صورت وجود در الگوی پیام جایگذاری می‌شوند</param>
    public class InvalidValueObjectStateException : DomainStateException
    {
        public InvalidValueObjectStateException(string message, params string[] parameters) : base(message)
        {
            Parameters = parameters;
        }
    }
}