using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Zamin.Extentions.Serializers.Abstractions;

namespace Zamin.Extensions.Serializers.NewtonSoft.Services;

public class NewtonSoftSerializer : IJsonSerializer, IDisposable
{
    private readonly ILogger<NewtonSoftSerializer> _logger;

    public NewtonSoftSerializer(ILogger<NewtonSoftSerializer> logger)
    {
        _logger = logger;
        _logger.LogInformation("Newton Soft Serializer Start working");
    }

    public TOutput Deserialize<TOutput>(string input)
    {
        _logger.LogTrace("Newton Soft Serializer Deserialize with name {input}", input);

        return string.IsNullOrWhiteSpace(input) ? default : JsonConvert.DeserializeObject<TOutput>(input);
    }

    public object Deserialize(string input, Type type)
    {
        _logger.LogTrace("Newton Soft Serializer Deserialize with name {input} and type {type}", input, type);

        return string.IsNullOrWhiteSpace(input) ? default : JsonConvert.DeserializeObject(input, type);
    }

    public string Serialize<TInput>(TInput input)
    {
        _logger.LogTrace("Newton Soft Serializer Serilize with name {input}", input);

        return input == null ? string.Empty : JsonConvert.SerializeObject(input, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
    }

    public void Dispose() => _logger.LogInformation("Newton Soft Serializer Stop working");
}
