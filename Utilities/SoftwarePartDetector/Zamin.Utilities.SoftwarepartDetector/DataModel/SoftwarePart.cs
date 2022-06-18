namespace Zamin.Utilities.SoftwarePartDetector.DataModel;
public class SoftwarePart
{
    public string Name { get; set; }=String.Empty;
    public SoftwarePartType SoftwarePartType { get; set; }
    public List<SoftwarePart> Children { get; set; } = new List<SoftwarePart>();
}
