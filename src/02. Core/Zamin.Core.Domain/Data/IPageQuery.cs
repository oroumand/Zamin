using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamin.Core.Domain.Data
{
    public interface IPageQuery
    {
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 
        public int SkipCount => (PageNumber - 1) * PageSize;
        public bool NeedTotalCount { get; set; }
        public string SortBy { get; set; }
        public bool SortAscending { get; set; }
    }
}
