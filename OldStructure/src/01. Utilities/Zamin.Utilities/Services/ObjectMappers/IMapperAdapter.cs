namespace Zamin.Utilities.Services.ObjectMappers;
public interface IMapperAdapter
{
    TDestination Map<TSource, TDestination>(TSource source);
}
