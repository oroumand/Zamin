namespace Zamin.Core.Contracts.Data.Commands;
/// <summary>
/// تعریف Interface برای الگوی UnitOfWork جهت مدیریت تراکنش‌ها با دیتابیس در این قسمت انجام شده است
/// تعریف کامل این الگو در کتاب P of EAA وجود دارد و تعریف اولیه را در آدرس زیر می‌توان مشاهده کرد
/// https://martinfowler.com/eaaCatalog/unitOfWork.html
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// در صورت نیاز به کنترل تراکنش‌ها از این متد جهت شروع تراکنش استفاده می‌شود.
    /// </summary>
    void BeginTransaction();

    /// <summary>
    /// در صورت کنترل دستی تراکنش از این متد جهت پایان موفقیت آمیز تراکنش استفاده می‌شود.
    /// </summary>
    void CommitTransaction();

    /// <summary>
    /// در صورت بروز خطا در فرایند‌ها از این متد جهت بازگشت تغییرات استفاده می‌شود.
    /// </summary>
    void RollbackTransaction();

    /// <summary>
    /// برای تایید تراکنشی که اتوماتیک توسط سیستم ایجاد شده است از این متد استفاده می‌شود.
    /// </summary>
    /// <returns></returns>
    int Commit();

    /// <summary>
    /// برای تایید تراکنشی که اتوماتیک توسط سیستم ایجاد شده است از این متد استفاده می‌شود.
    /// </summary>
    /// <returns></returns>
    Task<int> CommitAsync();
}
