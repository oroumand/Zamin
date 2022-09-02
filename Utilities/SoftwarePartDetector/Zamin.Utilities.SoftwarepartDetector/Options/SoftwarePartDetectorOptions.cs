namespace Zamin.Utilities.SoftwarePartDetector.Options;

public class SoftwarePartDetectorOptions
{
    public string ApplicationName { get; set; } = string.Empty;
    public string ModuleName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public string DestinationServiceBaseAddress { get; set; } = string.Empty;
    public string DestinationServicePath { get; set; } = string.Empty;
    public bool FakeSSL { get; set; } = false;
    public OAuthOption OAuth { get; set; }
}

public class OAuthOption
{
    public bool Enabled { get; set; } = true;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string Authority { get; set; } = string.Empty;
    public string[] Scopes { get; set; } = Array.Empty<string>();
}