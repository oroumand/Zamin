using Zamin.Utilities.SoftwarePartDetector.DataModel;

namespace Zamin.Utilities.SoftwarePartDetector.Publishers;

public interface ISoftwarePartPublisher
{
    Task Publish(SoftwarePart softwarePart);
}