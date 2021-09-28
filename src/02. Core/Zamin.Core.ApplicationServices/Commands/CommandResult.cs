using Zamin.Core.ApplicationServices.Common;

namespace Zamin.Core.ApplicationServices.Commands
{
    /// <summary>
    /// نتیجه انجام هر عملیات به کمک این کلاس بازگشت داده می‌شود.
    /// دلایل استفاده از این الگو و پیاده سازی کاملی از این الگو را در لینک زیر می‌توانید مشاهده کنید
    /// https://github.com/vkhorikov/CqrsInPractice
    /// </summary>
    public class CommandResult : ApplicationServiceResult
    {

    }

    public class CommandResult<TData> : CommandResult
    {
        internal TData _data;
        public TData Data => _data;
    }
}
