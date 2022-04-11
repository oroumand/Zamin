using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Zamin.Utilities.SoftwarePartDetector.DataModel;
namespace Zamin.Utilities.SoftwarePartDetector;
public class ControllersAndActionDetector
{
    private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

    public ControllersAndActionDetector(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
    {
        _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
    }
    public Task<List<SoftwarePart>> Detect()
    {
        return Task.Run(() =>
        {
            var ControllerActionDescriptors = _actionDescriptorCollectionProvider.ActionDescriptors.Items.OfType<ControllerActionDescriptor>().ToList();
            List<SoftwarePart> controllers =
                ControllerActionDescriptors.Select(c => c.ControllerName).Distinct().Select(c => new SoftwarePart
                {
                    Name = c,
                    ApplicationPartType = SoftwarePartType.Controller
                }).ToList();


            controllers = controllers.GroupJoin(ControllerActionDescriptors, c => c.Name, a => a.ControllerName, (c, a) => new SoftwarePart
            {
                Name = c.Name,
                ApplicationPartType= SoftwarePartType.Controller,
                Children = a.Select(b => new SoftwarePart
                {
                    Name = b.ActionName,
                    ApplicationPartType = SoftwarePartType.Action
                }).ToList()
            }).ToList();

            return controllers;
        });
    }
}
