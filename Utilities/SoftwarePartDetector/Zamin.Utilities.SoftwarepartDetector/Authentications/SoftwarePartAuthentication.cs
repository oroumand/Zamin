using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Zamin.Utilities.SoftwarePartDetector.Options;

namespace Zamin.Utilities.SoftwarePartDetector.Authentications;

public class SoftwarePartAuthentication : ISoftwarePartAuthentication
{
    private readonly HttpClient _httpClient;
    private readonly SoftwarePartDetectorOptions _softwarePartDetectorOption;

    public SoftwarePartAuthentication(HttpClient httpClient, IOptions<SoftwarePartDetectorOptions> softwarePartDetectorOption)
    {
        _httpClient = httpClient;
        _softwarePartDetectorOption = softwarePartDetectorOption.Value;
    }

    public async Task<TokenResponse> LoginAsync()
    {
        var discoveryDocument = await _httpClient.GetDiscoveryDocumentAsync(_softwarePartDetectorOption.OAuth.Authority);

        if (discoveryDocument.IsError)
            throw new Exception(discoveryDocument.Error);

        var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = _softwarePartDetectorOption.OAuth.ClientId,
                ClientSecret = _softwarePartDetectorOption.OAuth.ClientSecret,
                Scope = string.Join(',', _softwarePartDetectorOption.OAuth.Scopes),
            });

        if (tokenResponse.IsError)
            throw new Exception(tokenResponse.Error);

        return tokenResponse;
    }
}
