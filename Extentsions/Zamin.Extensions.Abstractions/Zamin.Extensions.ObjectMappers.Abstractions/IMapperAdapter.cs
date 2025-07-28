namespace Zamin.Extensions.ObjectMappers.Abstractions;
public interface IMapperAdapter
{
    TDestination Map<TSource, TDestination>(TSource source);
    
}
