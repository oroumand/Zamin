using Microsoft.Extensions.Options;
using Serilog.Core;
using Serilog.Events;
using Zamin.Utilities.SerilogRegistration.Options;

namespace Zamin.Utilities.SerilogRegistration.Sample.SampleEnrichers;
public class Sample01Enricher : ILogEventEnricher
{
    private readonly SerilogApplicationEnricherOptions _options;
    public Sample01Enricher(IOptions<SerilogApplicationEnricherOptions> options)
    {
        _options = options.Value;
    }
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var MyFirstNameProperty = propertyFactory.CreateProperty("MyFirstName", "Alireza");
        var MyLastNameProperty = propertyFactory.CreateProperty("MyLastName", "Oroumand");
        logEvent.AddPropertyIfAbsent(MyFirstNameProperty);
        logEvent.AddPropertyIfAbsent(MyLastNameProperty);
    }
}
