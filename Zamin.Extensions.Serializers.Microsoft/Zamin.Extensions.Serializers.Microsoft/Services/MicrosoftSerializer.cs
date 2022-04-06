using Microsoft.Extensions.Logging;
using System.Text.Json;
using Zamin.Extentions.Serializers.Abstractions;

namespace Zamin.Extensions.Serializers.Microsoft.Services;

public class MicrosoftSerializer : IJsonSerializer, IDisposable
{
    private readonly ILogger<MicrosoftSerializer> _logger;

    public MicrosoftSerializer(ILogger<MicrosoftSerializer> logger)
    {
        _logger = logger;
        _logger.LogInformation("Microsoft Serializer Start working");
    }

    public TOutput Deserialize<TOutput>(string input)
    {
        _logger.LogTrace("Microsoft Serializer Deserialize with name {input}", input);

        return string.IsNullOrWhiteSpace(input) ?
            default : JsonSerializer.Deserialize<TOutput>(input, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public object Deserialize(string input, Type type)
    {
        _logger.LogTrace("Microsoft Serializer Deserialize with name {input} and type {type}", input, type);

        return string.IsNullOrWhiteSpace(input) ?
            default : JsonSerializer.Deserialize(input, type, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public string Serilize<TInput>(TInput input)
    {
        _logger.LogTrace("Microsoft Serializer Serilize with name {input}", input);

        return input == null ? string.Empty : JsonSerializer.Serialize(input, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });
    }

    public void Dispose()
    {
        _logger.LogInformation("Microsoft Serializer Stop working");
    }
}
