using System.Collections.Generic;

namespace Zamin.Core.Domain.Data
{
    public class PagedData<T>
    {
        public List<T> QueryResult { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }

    }
}
