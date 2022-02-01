using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Zamin.Infra.Auth.ControllerDetectors.Models;
using Zamin.Utilities.Configurations;

namespace Zamin.Infra.Auth.AppPartsServices.ASPServices;

public class ApplicationPartDetector
{
    private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
    private readonly ZaminConfigurationOptions _zaminConfigurationOptions;

    public ApplicationPartDetector(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider,
                                   ZaminConfigurationOptions zaminConfigurationOptions)
    {
        _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        _zaminConfigurationOptions = zaminConfigurationOptions;
    }
    public ApplicationData? Detect()
    {

        var ControllerActionDescriptors = _actionDescriptorCollectionProvider.ActionDescriptors.Items.OfType<ControllerActionDescriptor>().ToList();

        List<ControllerData> controllersDatas = ControllerActionDescriptors.Select(c => c.ControllerName).Distinct().Select(c => new ControllerData { Name = c }).ToList();

        controllersDatas = controllersDatas.GroupJoin(ControllerActionDescriptors, c => c.Name, a => a.ControllerName, (c, a) => new ControllerData
        {
            Name = c.Name,
            ActionDatas = a.Select(b => new ActionData
            {
                Name = b.ActionName
            }).ToList()
        }).ToList();

        return controllersDatas != null && controllersDatas.Count > 0 ? new ApplicationData
        {
            ServiceName = _zaminConfigurationOptions.ServiceName,
            ControllerDatas = controllersDatas
        } : null;


    }
}
