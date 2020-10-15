using System.Collections.Generic;

namespace Zamin.Toolkits.Services.Logger
{
    public interface IScopeInformation
    {
        Dictionary<string, string> HostScopeInfo { get; }
        Dictionary<string, string> RequestScopeInfo { get; }
    }
}
