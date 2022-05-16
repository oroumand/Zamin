using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.Serializers.Asposes.Services;
using Zamin.Extentions.Serializers.Abstractions;

namespace Zamin.Extensions.DependencyInjection;

public static class AsposesExcelSerializerServiceCollectionExtensions
{
    public static IServiceCollection AddAsposesExcelSerializer(this IServiceCollection services)
        => services.AddSingleton<IExcelSerializer, AsposesExcelSerializer>();
}