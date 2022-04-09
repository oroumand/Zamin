using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.Serializers.NewtonSoft.Services;
using Zamin.Extentions.Serializers.Abstractions;

namespace Zamin.Extensions.Serializers.NewtonSoft.Extensions.DependencyInjection;

public static class NewtonSoftSerializerServiceCollectionExtensions
{
    public static IServiceCollection AddNewtonSoftSerializer(this IServiceCollection services)
        => services.AddSingleton<IJsonSerializer, NewtonSoftSerializer>();
}