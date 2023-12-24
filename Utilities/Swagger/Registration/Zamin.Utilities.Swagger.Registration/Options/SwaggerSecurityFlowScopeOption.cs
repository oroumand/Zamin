namespace Zamin.Utilities.Swagger.Registration.Options;

public class SwaggerSecurityFlowScopeOption
{
    public bool Enabled { get; set; } = true;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = string.Empty;

    public KeyValuePair<string, string> ToKeyValuePair(int priority)
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new ArgumentNullException($"SwaggerSecurity{priority} Scope name is null.");

        return new KeyValuePair<string, string>(Name, Description);
    }
}