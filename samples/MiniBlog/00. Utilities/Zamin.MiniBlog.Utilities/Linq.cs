using System.Linq;
using Zamin.Core.Domain.Data;
using Zamin.Utilities.Extensions;

namespace Zamin.MiniBlog.Utilities
{
    public static class Linq
    {
        public static PagedData<T> ToPageData<T>(this IQueryable<T> query, IPageQuery pageQuery)
        {
            var pageData = new PagedData<T>();

            if (pageQuery.PageNumber >= 0)
                query = query.Skip((pageQuery.PageNumber - 1) * pageData.PageSize);


            if (pageQuery.PageSize >= 0)
                query = query.Take(pageData.PageSize);

            if (pageQuery.SortBy != null)
                query = query.OrderByField(pageQuery.SortBy, pageQuery.SortAscending);



            pageData.QueryResult = query.ToList();
            pageData.PageNumber = pageQuery.PageNumber;
            pageData.PageSize = pageQuery.PageSize;
            pageData.TotalCount = pageQuery.NeedTotalCount ? query.Count() : -1;

            return pageData;
        }
    }
}