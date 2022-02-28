namespace Zamin.Core.Contracts.ApplicationServices.Commands;

/// <summary>
/// جهت علامت گذاری کلاسی که پارامتر‌های ورودی درستور را دارد از این اینترفیس اسفتاده می‌شود
/// </summary>
public interface ICommand
{
}

/// <summary>
/// جهت علامت گذاری کلاسی که پارامتر‌های ورودی درستور را دارد از این اینترفیس اسفتاده می‌شود
/// اگر به ازای دستور ارسال شده مقدار خروجی باید بازگشت داده شود از این اینترفیس استفاده می‌شود
/// </summary>
/// <typeparam name="TData">نوع داده‌ای که در ازای دستور باید بازگشت داده شود</typeparam>
public interface ICommand<TData>
{
}
