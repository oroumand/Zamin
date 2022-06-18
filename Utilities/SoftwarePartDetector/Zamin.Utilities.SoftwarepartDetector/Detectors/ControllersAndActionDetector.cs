using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Zamin.Utilities.SoftwarePartDetector.Attributes;
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
            var controllerActionDescriptors = _actionDescriptorCollectionProvider.ActionDescriptors.Items.OfType<ControllerActionDescriptor>().ToList();

            List<SoftwarePart> controllers =
                controllerActionDescriptors.Select(c => c.ControllerName).Distinct().Select(c => new SoftwarePart
                {
                    Name = c,
                    SoftwarePartType = SoftwarePartType.Controller
                }).ToList();

            controllers = controllers.GroupJoin(controllerActionDescriptors, c => c.Name, a => a.ControllerName, (c, a) => new SoftwarePart
            {
                Name = c.Name,
                SoftwarePartType = SoftwarePartType.Controller,
                Children = a.Select(b => new SoftwarePart
                {
                    Name = b.ActionName,
                    SoftwarePartType = SoftwarePartType.Action
                }).ToList()
            }).ToList();

            return controllers;
        });
    }

    public Task<List<SoftwarePartController>> Detect2()
    {
        return Task.Run(() =>
        {
            var result = new List<SoftwarePartController>();

            var controllerActionDescriptors = _actionDescriptorCollectionProvider.ActionDescriptors.Items.OfType<ControllerActionDescriptor>().ToList();

            foreach (var item in controllerActionDescriptors)
            {
                var attribute = item.ControllerTypeInfo.GetCustomAttributes(typeof(SoftwarePartDetectorAttribute), false).FirstOrDefault() as SoftwarePartDetectorAttribute;
                result.Add(new SoftwarePartController
                {
                    Module = attribute?.Module,
                    Service = attribute?.Service,
                    Name = item.ControllerName,
                    ApplicationPartType = SoftwarePartType.Controller,
                });
            }

            result = result.DistinctBy(c => c.Name).GroupJoin(controllerActionDescriptors, c => c.Name, a => a.ControllerName, (c, a) => new SoftwarePartController
            {
                Module = c.Module,
                Service = c.Service,
                Name = c.Name,
                ApplicationPartType = SoftwarePartType.Controller,
                Actions = a.Select(b => new SoftwarePartAction
                {
                    Name = b.ActionName,
                    ApplicationPartType = SoftwarePartType.Action
                }).DistinctBy(c => c.Name).ToList()
            }).ToList();

            return result;
        });
    }
}
