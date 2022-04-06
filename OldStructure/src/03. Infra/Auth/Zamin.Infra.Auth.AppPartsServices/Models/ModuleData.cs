namespace Zamin.Infra.Auth.ControllerDetectors.Models;

public class ModuleData
{
    public Guid BusinessId { get; set; }
    public Guid ApplicationId { get; set; }
    public string ModuleName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;
    public string URL { get; set; } = string.Empty;
    public int SortOrder { get; set; } = 1;
    public List<ServiceData> ServiceDatas { get; set; } = new List<ServiceData>();
}