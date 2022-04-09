using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.Serializers.EPPlus.Services;
using Zamin.Extentions.Serializers.Abstractions;

namespace Zamin.Extensions.Serializers.EPPlus.Extensions.DependencyInjection;

public static class EPPlusExcelSerializerServiceCollectionExtensions
{
    public static IServiceCollection AddEPPlusExcelSerializer(this IServiceCollection services)
        => services.AddSingleton<IExcelSerializer, EPPlusExcelSerializer>();
}