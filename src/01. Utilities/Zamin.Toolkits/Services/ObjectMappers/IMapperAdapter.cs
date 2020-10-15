namespace Zamin.Toolkits.Services.ObjectMappers
{
    public interface IMapperAdapter
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
