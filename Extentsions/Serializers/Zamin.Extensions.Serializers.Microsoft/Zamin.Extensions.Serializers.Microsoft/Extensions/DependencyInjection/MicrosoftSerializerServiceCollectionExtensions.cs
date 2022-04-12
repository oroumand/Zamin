using Microsoft.Extensions.DependencyInjection;
using Zamin.Extensions.Serializers.Microsoft.Services;
using Zamin.Extentions.Serializers.Abstractions;

namespace Zamin.Extensions.DependencyInjection;

public static class MicrosoftSerializerServiceCollectionExtensions
{
    public static IServiceCollection AddMicrosoftSerializer(this IServiceCollection services)
        => services.AddSingleton<IJsonSerializer, MicrosoftSerializer>();
}
