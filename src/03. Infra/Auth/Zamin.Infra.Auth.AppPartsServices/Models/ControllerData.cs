namespace Zamin.Infra.Auth.ControllerDetectors.Models;

public class ControllerData
{
    public Guid BusinessId { get; set; }
    public Guid ApplicationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<ActionData> ActionDatas { get; set; } = new List<ActionData>();
    public bool IsNew { get; set; } = false;

}
