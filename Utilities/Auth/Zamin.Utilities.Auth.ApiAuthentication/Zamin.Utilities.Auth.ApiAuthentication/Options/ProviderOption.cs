namespace Zamin.Utilities.Auth.ApiAuthentication.Options;

public sealed class ProviderOption
{
    public bool Enabled { get; set; } = true;
    public bool IgnoreSSL { get; set; } = false;
    public string Scheme { get; set; } = default!;
    public string Authority { get; set; } = default!;
    public string? HttpClientFactoryName { get; set; }
    public TokenType TokenTypeSupport { get; set; } = TokenType.Jwt;
    public JwtTokenConfigOption JwtTokenConfig { get; set; } = new();
    public RefrenceTokenConfigOption RefrenceTokenConfig { get; set; } = new();
    public RegisterUserInfoClaimsOption RegisterUserInfoClaims { get; set; } = new();
    public EndpointsPathOption? EndpointsPath { get; set; } = null;
    public List<UserClaimConvertTypeRuleOption> UserClaimConvertTypeRules { get; set; } = [];
    public int Priority { get; set; } = 1;
}
