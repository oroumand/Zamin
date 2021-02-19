using System.Linq;
using System.Security.Claims;

namespace Zamin.EndPoints.Web.Extentions
{
    public static class ClaimExtensions
    {
        public static string GetClaim(this ClaimsPrincipal userClaimsPrincipal, string claimType)
        {
            return userClaimsPrincipal.Claims.FirstOrDefault((x) => x.Type == claimType)?.Value;
        }
    }
}