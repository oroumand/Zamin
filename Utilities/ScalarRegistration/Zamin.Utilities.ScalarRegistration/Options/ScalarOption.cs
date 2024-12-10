namespace Zamin.Utilities.ScalarRegistration.Options;

public class ScalarOption
{
    public bool Enabled { get; set; } = true;
    public string Name { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Version { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
}