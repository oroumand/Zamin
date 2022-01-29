//using Microsoft.AspNetCore.Mvc.Controllers;
//using Microsoft.AspNetCore.Mvc.Infrastructure;
//using Microsoft.Extensions.Configuration;
//using System.ComponentModel.DataAnnotations;

//namespace Zamin.Infra.Auth.AppPartsServices.ASPServices;
//public class ApplicationPartDetector
//{
//    private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
//    private readonly IControllerActivatorProvider _controllerActivatorProvider;


//    public ApplicationPartDetector(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider,
//        IControllerActivatorProvider controllerActivatorProvider, AccessDBContext accessDBContext)
//    {
//        _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
//        _controllerActivatorProvider = controllerActivatorProvider;
//        _accessDBContext = accessDBContext;
//    }
//    public ServiceData Detect(IConfiguration configuration)
//    {
//        ServiceData serviceData = new ServiceData
//        {
//            ServiceName = configuration.GetSection("MaadConfigurations").GetSection("ServiceId").Value,
//            Description = configuration.GetSection("MaadConfigurations").GetSection("PersianServiceName").Value
//        };
//        var ControllerAndActionList = new List<ControllerActionDescriptor>();
//        foreach (var item in _actionDescriptorCollectionProvider.ActionDescriptors.Items)
//        {
//            var controllerAction = item as ControllerActionDescriptor;
//            ControllerAndActionList.Add(controllerAction);
//        }
//        var controllerName = ControllerAndActionList.Select(c => c.ControllerName).Distinct().ToList();
//        foreach (var item in controllerName)
//        {
//            var controllerInformation = ControllerAndActionList.Where(c => c.ControllerName == item).Select(c => c.ControllerTypeInfo).FirstOrDefault();
//            var actions = ControllerAndActionList.Where(c => c.ControllerName == item).Select(d => new ActionData
//            {
//                ActionName = d.ActionName,
//                Description = d.MethodInfo.GetCustomAttributes(inherit: true).OfType<DisplayAttribute>().Select(c => c.Name).FirstOrDefault()
//            }).ToList();
//            ControllerData controllerData = new ControllerData
//            {
//                ControllerName = item,
//                Description = controllerInformation.GetCustomAttributes(inherit: true).OfType<DisplayAttribute>().Select(c => c.Name).FirstOrDefault(),
//                ActionDatas = actions
//            };
//            serviceData.controllerDatas.Add(controllerData);
//        }

//        return serviceData;
//    }

//    public void InitData(ServiceData serviceData)
//    {
//        var service = _accessDBContext.ServiceDatas.Where(c => c.ServiceName == serviceData.ServiceName).FirstOrDefault();
//        if (service != null)
//            _accessDBContext.ServiceDatas.Remove(service);
//        _accessDBContext.AddRange(serviceData);
//        _accessDBContext.SaveChanges();
//    }
//}
//}
