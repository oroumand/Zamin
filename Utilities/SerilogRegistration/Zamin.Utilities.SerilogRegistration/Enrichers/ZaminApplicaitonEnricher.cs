using Microsoft.Extensions.Options;
using Serilog.Core;
using Serilog.Events;
using System.Reflection;
using Zamin.Utilities.SerilogRegistration.Options;

namespace Zamin.Utilities.SerilogRegistration.Enrichers;
public class ZaminApplicaitonEnricher : ILogEventEnricher
{
    private readonly SerilogApplicationEnricherOptions _options;
    public ZaminApplicaitonEnricher(IOptions<SerilogApplicationEnricherOptions> options)
    {
        _options = options.Value;
    }
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {        
        var applicationNameProperty = propertyFactory.CreateProperty(nameof(_options.ApplicationName), _options.ApplicationName);
        var serviceNameProperty = propertyFactory.CreateProperty(nameof(_options.ServiceName), _options.ServiceName);
        var serviceVersionProperty = propertyFactory.CreateProperty(nameof(_options.ServiceVersion), _options.ServiceVersion);
        var serviceIdProperty = propertyFactory.CreateProperty(nameof(_options.ServiceId), _options.ServiceId);
        var machineNameProperty = propertyFactory.CreateProperty(nameof(Environment.MachineName), Environment.MachineName);
        var entryPointProperty = propertyFactory.CreateProperty("EntryPoint", Assembly.GetEntryAssembly().GetName().Name);

        logEvent.AddPropertyIfAbsent(applicationNameProperty);
        logEvent.AddPropertyIfAbsent(serviceNameProperty);
        logEvent.AddPropertyIfAbsent(serviceVersionProperty);
        logEvent.AddPropertyIfAbsent(machineNameProperty);
        logEvent.AddPropertyIfAbsent(entryPointProperty);
        logEvent.AddPropertyIfAbsent(serviceIdProperty);

    }
}
