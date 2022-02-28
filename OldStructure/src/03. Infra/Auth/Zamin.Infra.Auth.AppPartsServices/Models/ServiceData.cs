namespace Zamin.Infra.Auth.ControllerDetectors.Models;

public class ServiceData
{
    public Guid BusinessId { get; set; }
    public Guid ModuleId { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;
    public string URL { get; set; } = string.Empty;
    public int SortOrder { get; set; } = 1;
    public List<ControllerData> ControllerDatas { get; set; } = new List<ControllerData>();
}
