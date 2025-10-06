using Mapster;
using Microsoft.Extensions.Logging;

namespace Zamin.Extensions.ObjectMappers.Mapster.Services;

public class Mapster : Abstractions.IMapperAdapter,IObjectMapper
{
    private readonly ILogger<Mapster> _logger;
    private readonly TypeAdapterConfig _config;

    public Mapster(ILogger<Mapster> logger)
    {
        _logger = logger;
        _config = TypeAdapterConfig.GlobalSettings;
        _logger.LogInformation("Mapster Adapter Start working");
    }

    public TDestination Map<TDestination>(object source)
    {
        _logger.LogTrace("AutoMapper Adapter Map {source} To {destination} with data {sourcedata}",
            typeof(object),
            typeof(TDestination),
            source);
        return source.Adapt<TDestination>(_config);
    }

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        _logger.LogTrace("AutoMapper Adapter Map {source} To {destination} with data {sourcedata}",
            typeof(TSource),
            typeof(TDestination),
            source);
        return source.Adapt<TSource, TDestination>(_config);
    }

    public IEnumerable<TDestination> MapCollection<TSource, TDestination>(IEnumerable<TSource> source)
    {
        _logger.LogTrace("AutoMapper Adapter Map {source} To {destination} with data {sourcedata}",
            typeof(TSource),
            typeof(TDestination),
            source);
        return source.Adapt<IEnumerable<TDestination>>(_config);
    }

    public void RegisterMap<TSource, TDestination>(Action<TypeAdapterSetter<TSource, TDestination>> configAction)
    {
        _logger.LogTrace("RegisterMap Adapter Map {source} To {destination}",
            typeof(TSource),
            typeof(TDestination));
        var setter = _config.NewConfig<TSource, TDestination>();
        configAction(setter);
    }
}