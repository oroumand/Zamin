using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Zamin.Extentions.ObjectMappers.Abstractions;
using Zamin.Extentions.Serializers.Abstractions;

namespace Zamin.Extensions.ObjectMappers.AutoMapper.Services;

public class AutoMapperAdapter : IMapperAdapter
{
    private readonly IMapper _mapper;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly ILogger<AutoMapperAdapter> _logger;

    public AutoMapperAdapter(IMapper mapper,
                             IJsonSerializer jsonSerializer,
                             ILogger<AutoMapperAdapter> logger)
    {
        _mapper = mapper;
        _jsonSerializer = jsonSerializer;
        _logger = logger;
        _logger.LogInformation("AutoMapper Adapter Start working");
    }

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        _logger.LogTrace("AutoMapper Adapter Map {source} To {destination} with data {sourcedata}",
                         typeof(TSource),
                         typeof(TDestination),
                         _jsonSerializer.Serialize(source));

        return _mapper.Map<TDestination>(source);
    }
}