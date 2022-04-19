namespace Zamin.Core.Contracts.ApplicationServices.Commands;
/// <summary>
/// تعریف ساختار برای مدیریت دستورات. پیاده سازی الگوی Mediator
/// از این الگو جهت کاهش پیچیدگی صدا زدن دستورات استفاده می‌شود
/// </summary>
public interface ICommandDispatcher
{
    /// <summary>
    /// یک دستور از نوع ICommand را دریافت کرده و پیاده سازی مناسب جهت مدیریت این دستور را یافته و کار را برای ادامه پردازش به آن پیاده سازی تحویل می‌شود.
    /// </summary>
    /// <typeparam name="TCommand">نوع دستور را تعیین می‌کند</typeparam>
    /// <param name="command">نام دستور</param>
    /// <returns></returns>
  
    Task<CommandResult> Send<TCommand>(TCommand command) where TCommand : class, ICommand;
    /// <summary>
    /// یک دستور از نوع ICommand را دریافت کرده و پیاده سازی مناسب جهت مدیریت این دستور را یافته و کار را برای ادامه پردازش به آن پیاده سازی تحویل می‌شود.
    /// </summary>
    /// <typeparam name="TCommand">نوع دستور را تعیین می‌کند</typeparam>
    /// <typeparam name="TData">نوع داده ای که از دستور بازگشت داده می‌شود</typeparam>
    /// <param name="command">نام دستور</param>
    /// <returns></returns>
    Task<CommandResult<TData>> Send<TCommand, TData>(TCommand command) where TCommand : class, ICommand<TData>;
}

