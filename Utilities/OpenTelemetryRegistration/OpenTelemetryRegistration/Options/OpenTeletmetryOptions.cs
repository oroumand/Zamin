using OpenTelemetry;

namespace OpenTelemetryRegistration.Options;
public class OpenTeletmetryOptions
{
    public string ApplicationName { get; set; }
    public string ServiceName { get; set; }
    public string ServiceVersion { get; set; }
    public string ServiceId { get; set; }
    public string AgentHost { get; set; } = "localhost";
    public int AgentPort { get; set; } = 6831;
    public ExportProcessorType ExportProcessorType { get; set; } = ExportProcessorType.Simple;
    public int MaxPayloadSizeInBytes { get; set; } = 4096;
}
