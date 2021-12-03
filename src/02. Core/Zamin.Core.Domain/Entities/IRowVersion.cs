namespace Zamin.Core.Domain.Entities
{
    /// <summary>
    /// مدیریت همزمانی (concurrency) برای وقتی که چندین کاربر به صورت همزمان سعی در به روزرسانی یک موجودیت  (entity) داشته باشند
    /// با افزودن فیلد RowVersion 
    /// </summary>
    public interface IRowVersion
    {
    }
}
