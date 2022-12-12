using Microsoft.Extensions.Logging;
using System.Text.Json;
using Zamin.Extensions.Serializers.Abstractions;

namespace Zamin.Extensions.Serializers.Microsoft.Services;

public class MicrosoftSerializer : IJsonSerializer, IDisposable
{
    private readonly ILogger<MicrosoftSerializer> _logger;
    private readonly JsonSerializerOptions options= new() { PropertyNameCaseInsensitive = true };

public MicrosoftSerializer(ILogger<MicrosoftSerializer> logger)
    {
        _logger = logger;
        _logger.LogInformation("Microsoft Serializer Start working");
    }

    public TOutput Deserialize<TOutput>(string input)
    {
        _logger.LogTrace("Microsoft Serializer Deserialize with name {input}", input);

        return string.IsNullOrWhiteSpace(input) ?
            default : JsonSerializer.Deserialize<TOutput>(input, options);
    }

    public object Deserialize(string input, Type type)
    {
        _logger.LogTrace("Microsoft Serializer Deserialize with name {input} and type {type}", input, type);

        return string.IsNullOrWhiteSpace(input) ?
            default : JsonSerializer.Deserialize(input, type, options);
    }

    public string Serialize<TInput>(TInput input)
    {
        _logger.LogTrace("Microsoft Serializer Serilize with name {input}", input);

        return input == null ? string.Empty : JsonSerializer.Serialize(input, options);
    }

    public void Dispose()
    {
        _logger.LogInformation("Microsoft Serializer Stop working");
    }
}
