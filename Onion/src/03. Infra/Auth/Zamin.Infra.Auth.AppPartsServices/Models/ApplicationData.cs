namespace Zamin.Infra.Auth.ControllerDetectors.Models;
public class ApplicationData
{
    public Guid BusinessId { get; set; }
    public string ApplicationName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;
    public string URL { get; set; } = string.Empty;
    public List<ModuleData> ModuleDatas { get; set; } = new List<ModuleData>();
}
