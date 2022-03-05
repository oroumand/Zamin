using Zamin.Utilities.Services.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Zamin.Infra.Tools.Srlzr.NewtonSoft;
public class NewtonSoftSerializer : IJsonSerializer
{
    public string Serialize<TInput>(TInput input)
        => input == null ? string.Empty : JsonConvert.SerializeObject(input, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

    public string Serialize<TInput, TJsonOption>(TInput input, TJsonOption jsonOption)
    {
        if (typeof(TJsonOption) == typeof(JsonSerializerSettings))
        {
            var option = jsonOption as JsonSerializerSettings;

            if (option != null)
                return JsonConvert.SerializeObject(input, option);

            return JsonConvert.SerializeObject(input);
        }

        throw new NotSupportedException("Invalid TJsonOption Type ...!");
    }

    public TOutput Deserialize<TOutput>(string input) =>
        string.IsNullOrWhiteSpace(input) ? default : JsonConvert.DeserializeObject<TOutput>(input);

    public TOutput Deserialize<TOutput, TJsonOption>(string input, TJsonOption jsonOption)
    {
        if (typeof(TJsonOption) == typeof(JsonSerializerSettings))
        {
            var option = jsonOption as JsonSerializerSettings;

            if (option != null)
                return JsonConvert.DeserializeObject<TOutput>(input, option);

            return JsonConvert.DeserializeObject<TOutput>(input);
        }

        throw new NotSupportedException("Invalid TJsonOption Type ...!");
    }

    public object Deserialize(string input, Type type) =>
        string.IsNullOrWhiteSpace(input) ? null : JsonConvert.DeserializeObject(input, type);
}
