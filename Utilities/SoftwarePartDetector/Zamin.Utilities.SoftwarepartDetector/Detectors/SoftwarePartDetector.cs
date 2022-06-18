using Zamin.Utilities.SoftwarePartDetector.DataModel;

namespace Zamin.Utilities.SoftwarePartDetector;

public class SoftwarePartDetector
{
    private readonly ControllersAndActionDetector _controllersAndActionDetector;

    public SoftwarePartDetector(ControllersAndActionDetector controllersAndActionDetector)
    {
        _controllersAndActionDetector = controllersAndActionDetector;
    }

    public async Task<SoftwarePart> Detect(string softwareName)
    {
        if (string.IsNullOrWhiteSpace(softwareName))
            throw new ArgumentNullException(nameof(softwareName));

        var application = new SoftwarePart
        {
            Name = softwareName,
            SoftwarePartType = SoftwarePartType.Software
        };

        var controllers = await _controllersAndActionDetector.Detect2();

        foreach (var controller in controllers)
        {
            if (controller.Module is null)
            {
                if (controller.Service is null)
                {
                    AddControllerAndActionsToParent(application, controller);
                }
                else
                {
                    var service = application.Children.FirstOrDefault(c => c.Name == controller.Service && c.SoftwarePartType == SoftwarePartType.Service);
                    if (service is not null)
                    {
                        AddControllerAndActionsToParent(service, controller);
                    }
                    else
                    {
                        service = new SoftwarePart()
                        {
                            Name = controller.Service,
                            SoftwarePartType = SoftwarePartType.Service,
                        };

                        AddControllerAndActionsToParent(service, controller);

                        application.Children.Add(service);
                    }
                }
            }
            else
            {
                AddControllerAndActionsToCustomModuleAndCustomService(application, controller);
            }
        }

        return application;
    }

    public async Task<SoftwarePart> Detect(string softwareName, string moduleName)
    {
        if (string.IsNullOrEmpty(moduleName))
            return await Detect(softwareName);

        var application = new SoftwarePart
        {
            Name = softwareName,
            SoftwarePartType = SoftwarePartType.Software,
            Children = new List<SoftwarePart>
            {
                new SoftwarePart
                {
                    Name = moduleName,
                    SoftwarePartType=SoftwarePartType.Module,
                }
            }
        };

        var controllers = await _controllersAndActionDetector.Detect2();

        foreach (var controller in controllers)
        {
            if (controller.Module is null)
            {
                var module = application.Children.First(c => c.Name == moduleName);

                if (controller.Service is null)
                {
                    AddControllerAndActionsToParent(module, controller);
                }
                else
                {
                    var service = module.Children.FirstOrDefault(c => c.Name == controller.Service);
                    if (service is not null)
                    {
                        AddControllerAndActionsToParent(service, controller);
                    }
                    else
                    {
                        service = CreateSoftwarePartService(controller.Service);

                        AddControllerAndActionsToParent(service, controller);

                        module.Children.Add(service);
                    }
                }

            }
            else
            {
                AddControllerAndActionsToCustomModuleAndCustomService(application, controller);
            }
        }

        return application;
    }

    public async Task<SoftwarePart> Detect(string softwareName, string moduleName, string serviceName)
    {
        if (string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(moduleName))
            return await Detect(softwareName, moduleName);

        var application = new SoftwarePart
        {
            Name = softwareName,
            SoftwarePartType = SoftwarePartType.Software,
            Children = new List<SoftwarePart>
            {
                new SoftwarePart
                {
                    Name = moduleName,
                    SoftwarePartType=SoftwarePartType.Module,
                    Children = new List<SoftwarePart>
                    {
                        new SoftwarePart
                        {
                            Name =serviceName,
                            SoftwarePartType= SoftwarePartType.Service,
                        }
                    }
                }
            }
        };

        var controllers = await _controllersAndActionDetector.Detect2();

        foreach (var controller in controllers)
        {
            if (controller.Module is null)
            {
                var module = application.Children.First(c => c.Name == moduleName);
                if (controller.Service is null)
                {
                    var service = module.Children.First(c => c.Name == serviceName);

                    AddControllerAndActionsToParent(service, controller);
                }
                else
                {
                    var service = module.Children.FirstOrDefault(c => c.Name == controller.Service);
                    if (service is not null)
                    {
                        AddControllerAndActionsToParent(service, controller);
                    }
                    else
                    {
                        service = CreateSoftwarePartService(controller.Service);

                        AddControllerAndActionsToParent(service, controller);

                        module.Children.Add(service);
                    }
                }
            }
            else
            {
                AddControllerAndActionsToCustomModuleAndCustomService(application, controller);
            }
        }

        return application;
    }

    private SoftwarePart CreateSoftwarePartModule(string name)
        => new()
        {
            Name = name,
            SoftwarePartType = SoftwarePartType.Module,
        };

    private SoftwarePart CreateSoftwarePartService(string name)
        => new()
        {
            Name = name,
            SoftwarePartType = SoftwarePartType.Service,
        };

    private void AddControllerAndActionsToParent(SoftwarePart parent, SoftwarePartController controller)
    {
        parent.Children.Add(new SoftwarePart()
        {
            Name = controller.Name,
            SoftwarePartType = SoftwarePartType.Controller,
            Children = controller.Actions.Select(action => new SoftwarePart()
            {
                Name = action.Name,
                SoftwarePartType = SoftwarePartType.Action
            }).ToList(),
        });
    }

    private void AddControllerAndActionsToCustomModuleAndCustomService(SoftwarePart application, SoftwarePartController controller)
    {
        var module = application.Children.FirstOrDefault(c => c.Name == controller.Module);
        if (module is not null)
        {
            var service = module.Children.FirstOrDefault(c => c.Name == controller.Service);
            if (service is not null)
            {
                AddControllerAndActionsToParent(service, controller);
            }
            else
            {
                service = CreateSoftwarePartService(controller.Service);

                AddControllerAndActionsToParent(service, controller);

                module.Children.Add(service);
            }
        }
        else
        {
            var service = CreateSoftwarePartService(controller.Service);

            AddControllerAndActionsToParent(service, controller);

            module = CreateSoftwarePartModule(controller.Module);

            module.Children.Add(service);

            application.Children.Add(module);
        }
    }
}
