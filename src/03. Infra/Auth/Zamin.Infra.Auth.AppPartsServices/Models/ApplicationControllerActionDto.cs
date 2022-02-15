using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamin.Infra.Auth.ControllerDetectors.Models;
public class ApplicationControllerActionDto
{
    public Guid ApplicationId { get; set; }
    public string ApplicationName { get; set; }
    public Guid ControllerId { get; set; }
    public string ControllerName { get; set; }
    public Guid ActionId { get; set; }
    public string ActionName { get; set; }
    public bool IsNewController { get; set; }=false;
    public bool IsNewAction { get; set; }=false;
}
