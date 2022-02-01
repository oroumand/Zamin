namespace Zamin.Infra.Auth.ControllerDetectors.Models;

public class ActionData
{
    public Guid Id { get; set; }
    public Guid ControllerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsNew { get; set; } = false;

}