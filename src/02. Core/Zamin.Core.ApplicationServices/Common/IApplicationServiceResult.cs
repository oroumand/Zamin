using System.Collections.Generic;

namespace Zamin.Core.ApplicationServices.Common
{
    public interface IApplicationServiceResult
    {
        IEnumerable<string> Messages { get; }
        ApplicationServiceStatus Status { get; set; }
    }
}