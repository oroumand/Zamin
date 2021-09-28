using System;
using System.Collections.Generic;
using System.Reflection;

namespace Zamin.Utilities.Services.Logger
{
    public class ScopeInformation : IScopeInformation
    {
        public ScopeInformation()
        {
            HostScopeInfo = new Dictionary<string, string>
            {
                {"MachineName", Environment.MachineName },
                {"EntryPoint", Assembly.GetEntryAssembly().GetName().Name }
            };

            RequestScopeInfo = new Dictionary<string, string>
            {
                { "RequestId", Guid.NewGuid().ToString() }
            };
        }

        public Dictionary<string, string> HostScopeInfo { get; }

        public Dictionary<string, string> RequestScopeInfo { get; }
    }

    public class ZaminEventId
    {
        public const int PerformanceMeasurement = 1001;
        public const int CommandValidation = 1010;
        public const int DomainValidationException = 1011;


    }
}
