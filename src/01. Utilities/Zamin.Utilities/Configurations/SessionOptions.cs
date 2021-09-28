using System;
using System.Linq;

namespace Zamin.Utilities.Configurations
{
    public class SessionOptions
    {
        public bool Enable { get; set; } = false;
        public SessionCookieOptions Cookie { get; set; } = new SessionCookieOptions();
        public TimeSpan IdleTimeout { get; set; }
        public TimeSpan IOTimeout { get; set; }
    }
    public class SessionCookieOptions
    {
        public string Name { get; set; } = ".AspNet.Session";
        public string Path { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public bool HttpOnly { get; set; } = true;
        public SameSiteModeOptions SameSite { get; set; } = SameSiteModeOptions.Lax;
        public CookieSecurePolicyOptions SecurePolicy { get; set; } = CookieSecurePolicyOptions.Always;
        public TimeSpan? Expiration { get; set; } = null;
        public TimeSpan? MaxAge { get; set; } = null;
        public bool IsEssential { get; set; } = true;

        public enum CookieSecurePolicyOptions
        {
            SameAsRequest = 0,
            Always = 1,
            None = 2
        }
        public enum SameSiteModeOptions
        {
            Unspecified = -1,
            None = 0,
            Lax = 1,
            Strict = 2
        }
    }

}
