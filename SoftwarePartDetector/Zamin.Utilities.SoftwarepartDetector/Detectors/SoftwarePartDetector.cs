

using Zamin.Utilities.SoftwarePartDetector.DataModel;

namespace Zamin.Utilities.SoftwarePartDetector;
public class SoftwarePartDetector
{
    private readonly ControllersAndActionDetector _controllersAndActionDetector;

    public SoftwarePartDetector(ControllersAndActionDetector controllersAndActionDetector)
    {
        _controllersAndActionDetector = controllersAndActionDetector;
    }
    public async Task<SoftwarePart> Detect(string ApplicationName)
    {
        if (string.IsNullOrWhiteSpace(ApplicationName))
            throw new ArgumentNullException(nameof(ApplicationName));
        return new SoftwarePart
        {
            Name = ApplicationName,
            ApplicationPartType = SoftwarePartType.Application,
            Children = await _controllersAndActionDetector.Detect()
        };
    }
    public async Task<SoftwarePart> Detect(string ApplicationName, string moduleName)
    {
        return string.IsNullOrEmpty(moduleName) ?
            await Detect(ApplicationName) :
            new SoftwarePart
            {
                Name = ApplicationName,
                ApplicationPartType = SoftwarePartType.Application,

                Children = new List<SoftwarePart>
                {
                    new SoftwarePart
                    {
                        Name = moduleName,
                        ApplicationPartType=SoftwarePartType.Module,
                        Children = await _controllersAndActionDetector.Detect()
                    }
                }
            };
    }
    public async Task<SoftwarePart> Detect(string ApplicationName, string moduleName, string serviceName)
    {
        return string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(moduleName) ?
            await Detect(ApplicationName, moduleName) :

            new SoftwarePart
            {
                Name = ApplicationName,
                ApplicationPartType = SoftwarePartType.Application,

                Children = new List<SoftwarePart>
                {
                    new SoftwarePart
                    {
                        Name = moduleName,
                        ApplicationPartType=SoftwarePartType.Module,
                        Children = new List<SoftwarePart>
                        {
                            new SoftwarePart
                            {
                                Name =serviceName,
                                ApplicationPartType= SoftwarePartType.Service,
                                Children = await _controllersAndActionDetector.Detect()
                            }
                        }
                    }
                }
            };
    }

}
