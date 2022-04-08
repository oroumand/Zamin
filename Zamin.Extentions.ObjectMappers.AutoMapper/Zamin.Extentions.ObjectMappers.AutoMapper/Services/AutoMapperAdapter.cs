using AutoMapper;
using Zamin.Extentions.ObjectMappers.Abstractions;

namespace Zamin.Extentions.ObjectMappers.AutoMapper.Services;

public class AutoMapperAdapter : IMapperAdapter
{
    private readonly MapperConfiguration _mapperConfiguration;
    private readonly IMapper _mapper;

    public AutoMapperAdapter(params Profile[] profiles)
    {
        _mapperConfiguration = new MapperConfiguration(c =>
        {
            foreach (var profile in profiles)
            {
                c.AddProfile(profile);
            }
        });

        _mapper = _mapperConfiguration.CreateMapper();
    }

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return _mapper.Map<TDestination>(source);
    }
}
