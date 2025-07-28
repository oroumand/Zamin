using Zamin.Extensions.ObjectMappers.Mapster.Sample.Dtos;
using Zamin.Extensions.ObjectMappers.Mapster.Sample.Model;
using Zamin.Extensions.ObjectMappers.Mapster.Services;

namespace Zamin.Extensions.ObjectMappers.Mapster.Sample.MapsterConfigs;

public static class MapsterConfig
{
    public static void RegisterMappings(IObjectMapper objectMapper)
    {
        objectMapper.RegisterMap<Person, PersonFullNameDto>(cfg =>
        {
            cfg.Map(dest => dest.FullName, src => $"{src.Name} {src.LastName}");
        });
    }
}