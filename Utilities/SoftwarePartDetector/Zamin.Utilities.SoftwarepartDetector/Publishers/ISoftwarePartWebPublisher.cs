using Zamin.Utilities.SoftwarePartDetector.DataModel;

namespace Zamin.Utilities.SoftwarePartDetector.Publishers;

public interface ISoftwarePartPublisher
{
    Task PublishAsync(SoftwarePart softwarePart);
}