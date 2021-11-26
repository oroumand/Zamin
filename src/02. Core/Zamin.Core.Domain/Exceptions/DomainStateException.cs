namespace Zamin.Core.Domain.Exceptions;

/// <summary>
/// خطاهای لایه Domain مربوط به Entityها و ValueObjectها به کمک Extention برای لایه‌های بالاتر ارسال می‌شود
/// با توجه به اینکه هم Entity و هم ValueObject به یک شکل خطا را ارسال می‌کنند یک کلاس Exception طراحی و پیاده سازی شده است.
/// برای اینکه در لایه‌های بالاتر بتوان تفاوت خطای و محل رخداد آن را تشخیص داد از الگوی MicroType استفاده شده.
/// </summary>
public abstract class DomainStateException : Exception
{
    /// <summary>
    /// لیست پارامتر‌های خطا
    /// در صورتی که پارامتری وجود داشته باشد message را به صورت الگو ارسال کرده و مقادیر پارامتر‌ها در محل مخصوص در الگو قرار می‌گیرند.
    /// </summary>
    public string[] Parameters { get; set; }

    public DomainStateException(string message, params string[] parameters) : base(message)
    {
        Parameters = parameters;

    }

    public override string ToString()
    {
        if (Parameters?.Length < 1)
        {
            return Message;
        }


        string result = Message;

        for (int i = 0; i < Parameters.Length; i++)
        {
            string placeHolder = $"{{{i}}}";
            result = Message.Replace(placeHolder, Parameters[i]);
        }

        return result;
    }
}