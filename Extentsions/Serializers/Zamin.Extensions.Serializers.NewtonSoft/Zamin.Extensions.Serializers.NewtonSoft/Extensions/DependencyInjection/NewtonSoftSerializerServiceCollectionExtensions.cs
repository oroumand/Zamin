using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.Serializers.NewtonSoft.Services;
using Zamin.Extentions.Serializers.Abstractions;

namespace Zamin.Extensions.DependencyInjection;

public static class NewtonSoftSerializerServiceCollectionExtensions
{
    public static IServiceCollection AddZaminNewtonSoftSerializer(this IServiceCollection services)
        => services.AddSingleton<IJsonSerializer, NewtonSoftSerializer>();
}