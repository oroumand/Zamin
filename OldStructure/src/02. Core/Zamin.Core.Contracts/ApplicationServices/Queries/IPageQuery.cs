namespace Zamin.Core.Contracts.ApplicationServices.Queries;
/// <summary>
/// اینترفیسی جهت استفاده به عنوان مارکر برای کلاس‌هایی که پارامتر‌های ورودی را برای جستجو تعیین می‌کنند!
/// در صورتی که جستجو نیاز به صفحه بندی داشته باشد از این اینترفیس استفاده می‌شود
/// </summary>
public interface IPageQuery<TData>:IQuery<TData>
{
    /// <summary>
    /// شماره صفحه ای که باید اطلاعات از آن بارگذاری شود
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// تعداد رکورد‌های هر صفحه 
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// تعداد رکورد‌هایی که باید از ابتدای نتیجه رد شود تا به رکوردهای مورد نظر برسیم
    /// </summary>
    public int SkipCount => (PageNumber - 1) * PageSize;

    /// <summary>
    /// تعیین اینکه آیا نیاز است تعداد کل رکورد‌های موجود در جستجو نیز بازگردانده شود یا خیر
    /// </summary>
    public bool NeedTotalCount { get; set; }

    /// <summary>
    /// تعیین ستونی که مرتب سازی بر اساس آن انجام می شود
    /// </summary>
    public string SortBy { get; set; }

    /// <summary>
    /// جهت مرتب سازی داده‌ها که به صورت صعودی انجام می‌شود یا نزولی
    /// </summary>
    public bool SortAscending { get; set; }
}
