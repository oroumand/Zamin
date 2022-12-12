using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.Serializers.Abstractions;
using Zamin.Extensions.Serializers.Microsoft.Services;

namespace Zamin.Extensions.DependencyInjection;

public static class MicrosoftSerializerServiceCollectionExtensions
{
    public static IServiceCollection AddZaminMicrosoftSerializer(this IServiceCollection services)
        => services.AddSingleton<IJsonSerializer, MicrosoftSerializer>();
}
