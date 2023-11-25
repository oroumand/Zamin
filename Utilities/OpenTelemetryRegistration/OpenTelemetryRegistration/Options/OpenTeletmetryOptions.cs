using OpenTelemetry;

namespace Zamin.Utilities.OpenTelemetryRegistration.Options;
public class OpenTeletmetryOptions
{
    public string ApplicationName { get; set; }
    public string ServiceName { get; set; }
    public string ServiceVersion { get; set; }
    public string ServiceId { get; set; }
    public ExportProcessorType ExportProcessorType { get; set; } = ExportProcessorType.Simple;
    public string OltpEndpoint { get; set; } = "http://localhost:4317";
    public double SamplingProbability { get; set; } = 0.25;
}
