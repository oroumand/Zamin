using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Zamin.Extentions.ObjectMappers.Abstractions;

namespace Zamin.Extentions.ObjectMappers.AutoMapper.Services;

public class AutoMapperAdapter : IMapperAdapter
{
    private readonly IMapper _mapper;
    private readonly ILogger<AutoMapperAdapter> _logger;

    public AutoMapperAdapter(IMapper mapper, ILogger<AutoMapperAdapter> logger)
    {
        _mapper = mapper;
        _logger = logger;
        _logger.LogInformation("AutoMapper Adapter Start working");
    }

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        _logger.LogTrace("AutoMapper Adapter Map {source} To {destination} with data {sourcedata}",
                       typeof(TSource),
                       typeof(TDestination),
                       JsonSerializer.Serialize(source));

        return _mapper.Map<TDestination>(source);
    }
}