using Mapster;

namespace Zamin.Extensions.ObjectMappers.Mapster.Services;

public interface IObjectMapper
{
    TDestination Map<TSource, TDestination>(TSource source);
    TDestination Map<TDestination>(object source);
    IEnumerable<TDestination> MapCollection<TSource, TDestination>(IEnumerable<TSource> source);
    void RegisterMap<TSource, TDestination>(Action<TypeAdapterSetter<TSource, TDestination>> configAction);
}