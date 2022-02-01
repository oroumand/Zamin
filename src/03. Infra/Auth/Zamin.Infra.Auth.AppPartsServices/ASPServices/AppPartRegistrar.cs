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

    private ApplicationData GetAppPart()
    {
        var result = _applicationPartDetector.Detect();
        if (result == null)
            throw new Exception($"Application parts detection failed for {_zaminConfiguration.SectionName}");
        return result;
    }

    private static bool ApplicatoinExists(List<ApplicationControllerActionDto> oldControllersAndAction)
    {
        return oldControllersAndAction != null && oldControllersAndAction.Count > 1;
    }

    private async Task InsertExistingApplication(ApplicationData AppPart, List<ApplicationControllerActionDto> oldControllersAndAction)
    {
        AppPart.Id = oldControllersAndAction.First().ApplicationId;
        AppPart.ControllerDatas.ForEach(controller =>
        {
            var oldController = Enumerable.FirstOrDefault(oldControllersAndAction, c => c.ControllerName == controller.Name);
            if (oldController != null)
            {
                controller.ApplicationId = AppPart.Id;
                controller.Id = oldController.ControllerId;
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

    private async Task InsertNewApplication(ApplicationData applicationData)
    {
        var id = await _appDataDbWrapper.InsertApplication();
        applicationData.Id = id;
        applicationData.ControllerDatas.ForEach(controller =>
        {
            SetNewControllerData(applicationData, controller);
        });
        await SaveControllersAndActions(applicationData);
    }

    private async Task SaveControllersAndActions(ApplicationData applicationData)
    {
        var controllerForInsert = applicationData.ControllerDatas.Where(c=>c.IsNew).ToList();
        var actionForInsert = applicationData.ControllerDatas.SelectMany(c => c.ActionDatas).Where(c=>c.IsNew).ToList();
        await _appDataDbWrapper.InsertNewControllers(controllerForInsert);
        await _appDataDbWrapper.InsertNewActions(actionForInsert);
    }

    private void SetNewControllerData(ApplicationData applicationData, ControllerData controller)
    {
        controller.ApplicationId = applicationData.Id;
        controller.Id = Guid.NewGuid();
        controller.IsNew = true;
        controller.ActionDatas.ForEach((ActionData action) =>
        {
            SetNewActionData(controller, action);
        });
    }

    private void SetNewActionData(ControllerData controller, ActionData action)
    {
        action.Id = Guid.NewGuid();
        action.ControllerId = controller.Id;
        action.IsNew = true;
    }
}
