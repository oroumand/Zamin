using Microsoft.Extensions.Options;
using Zamin.Utilities.SoftwarePartDetector.Options;
using Zamin.Utilities.SoftwarePartDetector.Publishers;

namespace Zamin.Utilities.SoftwarePartDetector.Services;

public class SoftwarePartDetectorService
{
    private readonly SoftwarePartDetector _softwarePartDetector;
    private readonly ISoftwarePartPublisher _partWebPublisher;
    private readonly SoftwarePartDetectorOptions _softwarePartDetectorOption;

    public SoftwarePartDetectorService(SoftwarePartDetector softwarePartDetector,
                                       ISoftwarePartPublisher partWebPublisher,
                                       IOptions<SoftwarePartDetectorOptions> softwarePartDetectorOption)
    {
        _softwarePartDetector = softwarePartDetector;
        _partWebPublisher = partWebPublisher;
        _softwarePartDetectorOption = softwarePartDetectorOption.Value;
    }
    public async Task Run()
    {
        if (string.IsNullOrEmpty(_softwarePartDetectorOption.ApplicationName))
            throw new ArgumentNullException("SoftwareName in SoftwarePartDetectorOption is null");

        var softwareParts = await _softwarePartDetector.Detect(_softwarePartDetectorOption.ApplicationName,
                                                               _softwarePartDetectorOption.ModuleName,
                                                               _softwarePartDetectorOption.ServiceName);
        if (softwareParts != null)
            await _partWebPublisher.PublishAsync(softwareParts);
    }
}
