namespace Zamin.Utilities.SoftwarePartDetector.DataModel;

public class SoftwarePartController
{
    public string? Module { get; set; }
    public string? Service { get; set; }
    public string Name { get; set; } = string.Empty;
    public SoftwarePartType ApplicationPartType { get; set; }
    public List<SoftwarePartAction> Actions { get; set; } = new();
}

public class SoftwarePartAction
{
    public string Name { get; set; } = string.Empty;
    public SoftwarePartType ApplicationPartType { get; set; }
}