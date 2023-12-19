namespace Zamin.Utilities.Auth.ApiAuthentication.Options;

public sealed class ApiAuthenticationOption
{
    public bool Enabled { get; set; } = true;
    public List<ProviderOption> Providers { get; set; } = [];

    public IReadOnlyList<ProviderOption> EnabledProviders => [.. Providers.Where(c => c.Enabled).OrderBy(c => c.Priority)];
    public ProviderOption? DefualtProvider => EnabledProviders.FirstOrDefault();
    public bool Active => Enabled && EnabledProviders.Any();
}