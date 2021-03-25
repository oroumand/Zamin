using System;
using System.Linq;

namespace Zamin.Utilities.Configurations
{

    public class SessionOptions
    {
        public bool Enable { get; set; }
        public SessionCookieOptions Cookie { get; set; }
        public TimeSpan IdleTimeout { get; set; }
        public TimeSpan IOTimeout { get; set; }
    }
    public class SessionCookieOptions
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Domain { get; set; }
        public bool HttpOnly { get; set; }
        public SameSiteModeOptions SameSite { get; set; }
        public CookieSecurePolicyOptions SecurePolicy { get; set; }
        public TimeSpan? Expiration { get; set; }
        public TimeSpan? MaxAge { get; set; }
        public bool IsEssential { get; set; }

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
