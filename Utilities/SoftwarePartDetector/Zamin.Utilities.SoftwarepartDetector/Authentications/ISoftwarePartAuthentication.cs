using IdentityModel.Client;

namespace Zamin.Utilities.SoftwarePartDetector.Authentications;

public interface ISoftwarePartAuthentication
{
    Task<TokenResponse> LoginAsync();
}