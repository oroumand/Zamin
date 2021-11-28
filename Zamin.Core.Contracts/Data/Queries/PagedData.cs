namespace Zamin.Core.Contracts.Data.Queries;
/// <summary>
/// ساختار پایه جهت بازگشت داده‌ها هنگام کوئری گرفتن وقتی که Paging دارد
/// </summary>
/// <typeparam name="T">نوع داده‌ای که از کوئری دریافت می‌شود را تعیین می‌کند!</typeparam>
public class PagedData<T>
{
    public List<T>? QueryResult { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalCount { get; set; }

}
