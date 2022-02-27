using Microsoft.Extensions.Logging;
using Zamin.Infra.Auth.ControllerDetectors.Data;
using Zamin.Infra.Auth.ControllerDetectors.Models;
using Zamin.Utilities.Configurations;

namespace Zamin.Infra.Auth.AppPartsServices.ASPServices;
public class AppPartRegistrar
{
    private readonly ControllerDetectorRepository _appDataDbWrapper;
    private readonly ApplicationPartDetector _applicationPartDetector;
    private readonly ILogger<AppPartRegistrar> _logger;
    private readonly ZaminConfigurationOptions _zaminConfiguration;

    public AppPartRegistrar(ControllerDetectorRepository appDataDbWrapper,
                               ApplicationPartDetector applicationPartDetector,
                               ILogger<AppPartRegistrar> logger,
                               ZaminConfigurationOptions zaminConfiguration)
    {
        _appDataDbWrapper = appDataDbWrapper;
        _applicationPartDetector = applicationPartDetector;
        _logger = logger;
        _zaminConfiguration = zaminConfiguration;
    }

    public async Task Register()
    {
        try
        {
            await StartRegistration();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Application parts detection Failed for {ServiceName}", _zaminConfiguration.SectionName);
        }
    }

    private async Task StartRegistration()
    {
        var AppPart = GetAppPart();

        var oldControllersAndAction = await _appDataDbWrapper.GetOldApplicationParts(_zaminConfiguration.ServiceName);

        if (ApplicatoinExists(oldControllersAndAction))
            await InsertExistingApplication(AppPart, oldControllersAndAction);
        else
            await InsertNewApplication(AppPart);



    }

    private ServiceData GetAppPart()
    {
        var result = _applicationPartDetector.Detect();
        if (result == null)
            throw new Exception($"Application parts detection failed for {_zaminConfiguration.SectionName}");
        return result;
    }

    private static bool ApplicatoinExists(List<ApplicationControllerActionDto> oldControllersAndAction)
    {
        return oldControllersAndAction != null && oldControllersAndAction.Count > 0;
    }

    private async Task InsertExistingApplication(ServiceData AppPart, List<ApplicationControllerActionDto> oldControllersAndAction)
    {
        AppPart.BusinessId = oldControllersAndAction.First().ServiceId;
        AppPart.ControllerDatas.ForEach(controller =>
        {
            var oldController = Enumerable.FirstOrDefault(oldControllersAndAction, c => c.ControllerName == controller.Name);
            if (oldController != null)
            {
                controller.ServiceId = AppPart.BusinessId;
                controller.BusinessId = oldController.ControllerId;
                controller.ActionDatas.ForEach(action =>
                {
                    if (!oldControllersAndAction.Any(c => c.ActionName == action.Name && c.ControllerName == controller.Name))
                    {
                        SetNewActionData(controller, action);
                    }
                });
            }
            else
            {
                SetNewControllerData(AppPart, controller);
            }
        });
        await SaveControllersAndActions(AppPart);
    }

    private async Task InsertNewApplication(ServiceData applicationData)
    {
        Guid modelId = await GetModelId();
        var id = await _appDataDbWrapper.InsertService(modelId);
        applicationData.BusinessId = id;
        applicationData.ControllerDatas.ForEach(controller =>
        {
            SetNewControllerData(applicationData, controller);
        });
        await SaveControllersAndActions(applicationData);
    }

    private async Task<Guid> GetModelId()
    {
        Guid applicationId = await GetApplicatoinId();
        ModuleData module = await _appDataDbWrapper.GetModuleData(applicationId, _zaminConfiguration.ModeleName);
        if (module == null)
        {
            var moduleId = await _appDataDbWrapper.InsertModuleData(applicationId, _zaminConfiguration.ModeleName);
            return moduleId;
        }
        return module.BusinessId;
    }

    private async Task<Guid> GetApplicatoinId()
    {
        ApplicationData applicationData = await _appDataDbWrapper.GetApplicationData(_zaminConfiguration.ApplicationName);
        if (applicationData is null)
        {
            var appId = await _appDataDbWrapper.InsertApplicationData(_zaminConfiguration.ApplicationName);
            return appId;
        }
        return applicationData.BusinessId;
    }

    private async Task SaveControllersAndActions(ServiceData applicationData)
    {
        var controllerForInsert = applicationData.ControllerDatas.Where(c => c.IsNew).ToList();
        var actionForInsert = applicationData.ControllerDatas.SelectMany(c => c.ActionDatas).Where(c => c.IsNew).ToList();
        await _appDataDbWrapper.InsertNewControllers(controllerForInsert);
        await _appDataDbWrapper.InsertNewActions(actionForInsert);
    }

    private void SetNewControllerData(ServiceData applicationData, ControllerData controller)
    {
        controller.ServiceId = applicationData.BusinessId;
        controller.BusinessId = Guid.NewGuid();
        controller.IsNew = true;
        controller.ActionDatas.ForEach((ActionData action) =>
        {
            SetNewActionData(controller, action);
        });
    }

    private void SetNewActionData(ControllerData controller, ActionData action)
    {
        action.BusinessId = Guid.NewGuid();
        action.ControllerId = controller.BusinessId;
        action.IsNew = true;
    }
}
