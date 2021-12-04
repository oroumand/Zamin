using Zamin.Core.Contracts.ApplicationServices.Common;

namespace Zamin.Core.Contracts.ApplicationServices.Commands;
/// <summary>
/// نتیجه انجام هر عملیات به کمک این کلاس بازگشت داده می‌شود.
/// دلایل استفاده از این الگو و پیاده سازی کاملی از این الگو را در لینک زیر می‌توانید مشاهده کنید
/// https://github.com/vkhorikov/CqrsInPractice
/// </summary>
public class CommandResult : ApplicationServiceResult
{

}
/// <summary>
/// نتیجه انجام هر عملیات به کمک این کلاس بازگشت داده می‌شود.
/// دلایل استفاده از این الگو و پیاده سازی کاملی از این الگو را در لینک زیر می‌توانید مشاهده کنید
/// این ساختار در صورتی استفاده میشود که برای عملیات مقدار خروجی نیاز باشد
/// https://github.com/vkhorikov/CqrsInPractice
/// </summary>
/// <typeparam name="TData">نوع داده‌ای که بازگشت داده می‌شود</typeparam>
public class CommandResult<TData> : CommandResult
{
    public TData? _data;
    public TData? Data => _data;
}

