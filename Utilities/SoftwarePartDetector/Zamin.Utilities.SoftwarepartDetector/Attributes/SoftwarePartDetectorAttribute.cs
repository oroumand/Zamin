namespace Zamin.Utilities.SoftwarePartDetector.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class SoftwarePartDetectorAttribute : Attribute
{
    private string? _module { get; set; }
    private string? _service { get; set; }

    public SoftwarePartDetectorAttribute(string? service , string? module = null)
    {
        if (string.IsNullOrWhiteSpace(service))
            throw new ArgumentNullException(nameof(service));

        _module = module;
        _service = service;
    }

    public string? Module => _module;
    public string? Service => _service;
}