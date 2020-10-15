using Zamin.Core.ApplicationServices.Common;

namespace Zamin.Core.ApplicationServices.Queries
{
    public sealed class QueryResult<TData> : ApplicationServiceResult
    {
        internal TData _data;
        public TData Data
        {
            get
            {
                return _data;
            }
        }
    }
}
